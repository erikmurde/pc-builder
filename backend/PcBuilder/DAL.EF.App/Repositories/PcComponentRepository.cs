using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.DTO.PcComponent;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PcComponentRepository :
    EFBaseRepository<PcComponentDTO, PcComponent, ApplicationDbContext>, IPcComponentRepository
{
    private readonly PcComponentMapper _mapper;
    
    public PcComponentRepository(ApplicationDbContext dataContext, PcComponentMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PcComponentDTO>> AllAsync(Guid pcBuildId)
    {
        return await RepositoryDbSet
            .Where(p => p.PcBuildId == pcBuildId)
            .Include(p => p.Component)
            .ThenInclude(c => c!.Category)
            .Select(p => new PcComponentDTO
            {
                CategoryName = p.Component!.Category!.CategoryName,
                ComponentName = p.Component!.ComponentName,
                ImageSrc = p.Component!.ImageSrc,
                Price = p.Component!.Price
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<PcComponentEditDTO>> AllAsyncEdit(Guid pcBuildId)
    {
        return await RepositoryDbSet
            .Where(p => p.PcBuildId == pcBuildId)
            .Include(p => p.Component)
            .ThenInclude(c => c!.Category)
            .Select(p => new PcComponentEditDTO
            {
                Id = p.Id,
                ComponentId = p.ComponentId,
                PcBuildId = p.PcBuildId,
                CategoryName = p.Component!.Category!.CategoryName,
                Qty = p.Qty
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<PcComponentSimpleDTO>> AllAsyncSimple(Guid pcBuildId)
    {
        return await RepositoryDbSet
            .Where(p => p.PcBuildId == pcBuildId)
            .Include(p => p.Component)
            .ThenInclude(c => c!.Category)
            .Select(p => new PcComponentSimpleDTO
            {
                ComponentId = p.ComponentId,
                CategoryName = p.Component!.Category!.CategoryName
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<PcComponentStockDTO>> AllAsyncStock(Guid pcBuildId)
    {
        return await RepositoryDbSet
            .Where(p => p.PcBuildId == pcBuildId)
            .Include(p => p.Component)
            .Select(p => new PcComponentStockDTO
            {
                ComponentId = p.ComponentId,
                Stock = p.Component!.Stock
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<PcComponentCartDTO>> AllAsyncCart(Guid pcBuildId)
    {
        return await RepositoryDbSet
            .Where(p => p.PcBuildId == pcBuildId)
            .Include(p => p.Component).ThenInclude(c => c!.Category)
            .Include(p => p.Component).ThenInclude(c => c!.Discount)
            .Select(p => new PcComponentCartDTO
            {
                ComponentId = p.ComponentId,
                CategoryName = p.Component!.Category!.CategoryName,
                DiscountPercentage = p.Component!.Discount!.DiscountPercentage,
                ComponentName = p.Component!.ComponentName,
                ImageSrc = p.Component!.ImageSrc,
                Stock = p.Component!.Stock,
                Price = p.Component!.Price
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<PcComponentStoreDTO>> AllAsyncStore(Guid pcBuildId)
    {
        return await RepositoryDbSet
            .Where(p => p.PcBuildId == pcBuildId)
            .Include(p => p.Component).ThenInclude(c => c!.Category)
            .Include(p => p.Component).ThenInclude(c => c!.Discount)
            .Select(p => new PcComponentStoreDTO
            {
                CategoryName = p.Component!.Category!.CategoryName,
                ComponentName = p.Component!.ComponentName,
                DiscountPercentage = p.Component!.Discount!.DiscountPercentage,
                Price = p.Component!.Price
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public PcComponentCreateDTO Add(PcComponentCreateDTO pcComponent)
    {
        return _mapper.MapCreate(RepositoryDbSet.Add(_mapper.MapCreate(pcComponent)).Entity);
    }

    public PcComponentEditDTO Update(PcComponentEditDTO pcComponent)
    {
        return _mapper.MapEdit(RepositoryDbSet.Update(_mapper.MapEdit(pcComponent)).Entity);
    }
}