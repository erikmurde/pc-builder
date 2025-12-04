using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Mappers;
using BLL.DTO.PcBuild;
using DAL.Contracts.App;
using DTO.Base;
using DAL.DTO.PcComponent;
using DAL.EF.App.Seeding.Names;
using Helpers.Base;

namespace BLL.App.Services;

public class PcBuildService : 
    BaseEntityService<PcBuildDTO, DAL.DTO.PcBuild.PcBuildDTO, IPcBuildRepository>, IPcBuildService
{
    private readonly IAppUOW _uow;
    private readonly PcBuildMapper _pcBuildMapper;
    private readonly PcComponentMapper _pcComponentMapper;
    private readonly UserReviewMapper _userReviewMapper;

    public PcBuildService(IAppUOW uow, 
        PcBuildMapper pcBuildMapper, PcComponentMapper pcComponentMapper, UserReviewMapper userReviewMapper) : 
        base(uow.PcBuildRepository, pcBuildMapper)
    {
        _uow = uow;
        _pcBuildMapper = pcBuildMapper;
        _pcComponentMapper = pcComponentMapper;
        _userReviewMapper = userReviewMapper;
    }

    public async Task<IEnumerable<PcBuildStoreDTO>> AllAsyncStore()
    {
        var dalPcBuilds = (await _uow.PcBuildRepository.AllAsyncStore())
            .ToList();

        return dalPcBuilds.Select(p =>
        {
            var bllPcBuild = _pcBuildMapper.MapStore(p);           
            
            // Add component names to PC
            bllPcBuild.PcComponents = _uow.PcComponentRepository
                .AllAsyncStore(bllPcBuild.Id).Result
                .Select(c => _pcComponentMapper.MapStore(c))
                .ToList();

            return bllPcBuild;
        }).ToList();
    }

    public async Task<PcBuildDetailsDTO?> FindAsyncDetails(Guid id)
    {
        var pcBuild = (await _uow.PcBuildRepository.FindAsyncDetails(id));
        if (pcBuild == null)
        {
            return null;
        }
        
        var bllPcBuild = _pcBuildMapper.MapDetails(pcBuild);
   
        var components = 
            await _uow.PcComponentRepository.AllAsync(pcBuild.Id);

        var userReviews = 
            await _uow.UserReviewRepository.AllAsyncPcBuild(pcBuild.Id);
            
        bllPcBuild.PcComponents = components
            .Select(c => _pcComponentMapper.Map(c))
            .ToList();

        bllPcBuild.UserReviews = userReviews
            .Select(u => _userReviewMapper.Map(u))
            .ToList();

        return bllPcBuild;
    }

    public async Task<PcBuildEditDTO?> FindAsyncEdit(Guid id)
    {
        var pcBuild = await _uow.PcBuildRepository.FindAsyncEdit(id);
        if (pcBuild == null)
        {
            return null;
        }

        var components = 
            (await _uow.PcComponentRepository.AllAsyncSimple(pcBuild.Id))
            .ToList();

        return _pcBuildMapper.MapEdit(pcBuild, components);
    }

    public async Task<PcBuildEditDTO> Update(PcBuildEditDTO pcBuild)
    {
        if (!EntityValidationHelper.ValidatePcBuild(pcBuild))
        {
            throw new ArgumentException("PcBuildEditDTO is invalid!");
        }
        if (!await ValidateDiscountAndCategory(pcBuild.DiscountId, pcBuild.CategoryId))
        {
            throw new ArgumentException("Invalid discount or category!");
        }

        var dalPcBuild = _pcBuildMapper.MapEdit(pcBuild);

        var pcComponents = 
            (await _uow.PcComponentRepository.AllAsyncEdit(dalPcBuild.Id))
            .ToList();
        
        var secondaryStorage = pcComponents
            .FirstOrDefault(p => p.CategoryName == CategoryNames.HardDrive);

        // Remove secondary storage if necessary
        if (secondaryStorage != null && pcBuild.SecondaryStorageId == null)
        {
            await _uow.PcComponentRepository.RemoveAsync(secondaryStorage.Id);
        }
        
        // Edit any PcComponents that have change
        foreach (var component in GetComponentsDictionary(dalPcBuild))
        {
            var pcComponent = pcComponents.FirstOrDefault(p => p.CategoryName == component.Value);

            // Component did not exist before, it has to be added
            if (pcComponent == null)
            {
                if (!await AddPcComponent(pcBuild.Id, component.Key, component.Value))
                {
                    throw new ArgumentException("Failed to add PcComponent to PcBuild!");
                }
                continue;
            }

            // Component has not changed
            if (pcComponent.ComponentId == component.Key) continue;
            
            if (!await EditPcComponent(pcComponent, component.Key, component.Value))
            {
                throw new ArgumentException("Failed to edit PcComponent!");
            }
        }
        
        _uow.PcBuildRepository.Update(dalPcBuild);

        await _uow.SaveChangesAsync();
        return _pcBuildMapper.MapEdit(dalPcBuild);
    }

    public async Task<PcBuildCreateDTO> Add(PcBuildCreateDTO pcBuild)
    {
        if (!EntityValidationHelper.ValidatePcBuild(pcBuild))
        {
            throw new ArgumentException("PcBuildCreateDTO is invalid!");
        }
        if (!await ValidateDiscountAndCategory(pcBuild.DiscountId, pcBuild.CategoryId))
        {
            throw new ArgumentException("Invalid discount or category!");
        }

        pcBuild.Id = Guid.NewGuid();
        var dalPcBuild = _pcBuildMapper.MapCreate(pcBuild);
        
        _uow.PcBuildRepository.Add(dalPcBuild);
        await _uow.SaveChangesAsync();

        // Add PcComponents to PC
        foreach (var component in GetComponentsDictionary(dalPcBuild))
        {
            if (!await AddPcComponent(dalPcBuild.Id, component.Key, component.Value))
            {
                // Delete PC build if creating components failed
                await RemoveAsync(dalPcBuild.Id);
                await _uow.SaveChangesAsync();
                
                throw new ArgumentException("Failed to add PcComponent to PcBuild!");
            }
        }
        
        await _uow.SaveChangesAsync();
        return _pcBuildMapper.MapCreate(dalPcBuild);
    }

    public override async Task<PcBuildDTO?> RemoveAsync(Guid id)
    {
        var dalPcBuild = await _uow.PcBuildRepository.FindAsync(id);
        if (dalPcBuild == null)
        {
            return null;
        }

        // Delete all PcComponents belonging to this PcBuild
        foreach (var pcComponent in await _uow.PcComponentRepository.AllAsyncEdit(dalPcBuild.Id))
        {
            await _uow.PcComponentRepository.RemoveAsync(pcComponent.Id);
        }

        var result = await _uow.PcBuildRepository.RemoveAsync(id);
        await _uow.SaveChangesAsync();
        
        return _pcBuildMapper.Map(result);
    }

    private static Dictionary<Guid, string> GetComponentsDictionary(PcBuildBaseDTO pcBuild)
    {
        var components = new Dictionary<Guid, string>
        {
            {pcBuild.CaseId, CategoryNames.Case}, 
            {pcBuild.MotherboardId, CategoryNames.Motherboard},
            {pcBuild.ProcessorId, CategoryNames.Processor}, 
            {pcBuild.CpuCoolerId, CategoryNames.CpuCooler},
            {pcBuild.MemoryId, CategoryNames.Memory}, 
            {pcBuild.GraphicsCardId, CategoryNames.GraphicsCard},
            {pcBuild.PrimaryStorageId, CategoryNames.SolidStateDrive}, 
            {pcBuild.PowerSupplyId, CategoryNames.PowerSupply}, 
            {pcBuild.OperatingSystemId, CategoryNames.OperatingSystem}
        };
        if (pcBuild.SecondaryStorageId != null)
        {
            components.Add(pcBuild.SecondaryStorageId.Value, CategoryNames.HardDrive);
        }
        return components;
    }

    private async Task<bool> AddPcComponent(
        Guid pcBuildId, Guid componentId, string requiredCategory)
    {
        if (!await ValidatePcComponent(componentId, requiredCategory)) return false;
            
        _uow.PcComponentRepository.Add(new PcComponentCreateDTO
        {
            PcBuildId = pcBuildId,
            ComponentId = componentId,
            Qty = 1
        });
        return true;
    }

    private async Task<bool> EditPcComponent(
        PcComponentEditDTO pcComponent, Guid componentId, string requiredCategory)
    {
        if (!await ValidatePcComponent(componentId, requiredCategory)) return false;

        pcComponent.ComponentId = componentId;
        _uow.PcComponentRepository.Update(pcComponent);
            
        return true;
    }

    private async Task<bool> ValidatePcComponent(Guid componentId, string requiredCategory)
    {
        var component = await _uow.ComponentRepository.FindAsyncSimple(componentId);
        return component != null && component.CategoryName == requiredCategory;
    }

    private async Task<bool> ValidateDiscountAndCategory(Guid discountId, Guid categoryId)
    {
        if (await _uow.DiscountRepository.FindAsync(discountId) == null)
        {
            return false;
        }
        return await _uow.CategoryRepository.FindAsync(categoryId) != null;
    }
}