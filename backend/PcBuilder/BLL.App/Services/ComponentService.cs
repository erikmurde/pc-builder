using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Component;
using BLL.DTO.Mappers;
using BLL.DTO.PcBuild;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class ComponentService : 
    BaseEntityService<ComponentDTO, DAL.DTO.Component.ComponentDTO, IComponentRepository>, IComponentService
{
    private readonly IAppUOW _uow;
    private readonly ComponentMapper _componentMapper;
    private readonly ComponentAttributeMapper _componentAttributeMapper;

    public ComponentService(
        IAppUOW uow, ComponentMapper componentMapper, ComponentAttributeMapper componentAttributeMapper) : 
        base(uow.ComponentRepository, componentMapper)
    {
        _uow = uow;
        _componentMapper = componentMapper;
        _componentAttributeMapper = componentAttributeMapper;
    }

    public async Task<IEnumerable<ComponentSimpleDTO>> AllAsyncSimple()
    {
        return (await _uow.ComponentRepository.AllAsyncSimple())
            .Select(c => _componentMapper.MapSimple(c));
    }
    
    public async Task<IEnumerable<ComponentDetailsDTO>> AllAsyncMotherboard()
    {
        var components = (await _uow.ComponentRepository.AllAsyncMotherboard())
            .Select(c => _componentMapper.MapDetails(c))
            .ToList();

        foreach (var component in components)
        {
            await AddComponentAttributes(component);
        }

        return components;
    }
    
    public async Task<IEnumerable<ComponentDetailsDTO>> AllAsyncSelected(PcBuildEditDTO values)
    {
        var components = new List<ComponentDetailsDTO>();

        var ids = new List<Guid> { 
            values.CaseId, values.MotherboardId, values.ProcessorId, values.CpuCoolerId, values.MemoryId,
            values.GraphicsCardId, values.PrimaryStorageId, values.PowerSupplyId, values.OperatingSystemId
        };
        
        if (values.SecondaryStorageId != null) ids.Add(values.SecondaryStorageId.Value);

        foreach (var id in ids)
        {
            var component = _componentMapper.MapDetails((
                await _uow.ComponentRepository.FindAsyncDetails(id))!);

            await AddComponentAttributes(component);
            components.Add(component);
        }

        return components;
    }

    public async Task<ComponentDetailsDTO?> FindAsyncDetails(Guid id)
    {
        var dalComponent = await _uow.ComponentRepository.FindAsyncDetails(id);
        if (dalComponent == null)
        {
            return null;
        }

        var component = _componentMapper.MapDetails(dalComponent);
        await AddComponentAttributes(component);
        
        return component;
    }

    public async Task<ComponentEditDTO?> FindAsyncEdit(Guid id)
    {
        var dalComponent = await _uow.ComponentRepository.FindAsyncEdit(id);
        return dalComponent == null ? null : _componentMapper.MapEdit(dalComponent);
    }

    public ComponentEditDTO Update(ComponentEditDTO component)
    {
        if (!EntityValidationHelper.ValidateComponent(component))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ComponentRepository.Update(_componentMapper.MapEdit(component));
        return _componentMapper.MapEdit(result);
    }

    public ComponentCreateDTO Add(ComponentCreateDTO component)
    {
        if (!EntityValidationHelper.ValidateComponent(component))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ComponentRepository.Add(_componentMapper.MapCreate(component));
        return _componentMapper.MapCreate(result);
    }

    public override async Task<ComponentDTO?> RemoveAsync(Guid id)
    {
        var dalComponent = await _uow.ComponentRepository.FindAsync(id);
        if (dalComponent == null)
        {
            return null;
        }
        
        // Delete all ComponentAttributes belonging to this Component
        foreach (var componentAttribute in await _uow.ComponentAttributeRepository.AllAsync(dalComponent.Id))
        {
            await _uow.ComponentAttributeRepository.RemoveAsync(componentAttribute.Id);
        }

        var result = await _uow.ComponentRepository.RemoveAsync(id);
        
        return _componentMapper.Map(result);
    }

    private async Task AddComponentAttributes(ComponentDetailsDTO component)
    {
        var componentAttributes = 
            await _uow.ComponentAttributeRepository.AllAsync(component.Id);

        component.ComponentAttributes = componentAttributes
            .Select(c => _componentAttributeMapper.Map(c))
            .ToList();
    }
}