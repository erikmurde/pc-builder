using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.DTO.ShippingCost;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ShippingCostRepository :
    EFBaseRepository<ShippingCostDTO, ShippingCost, ApplicationDbContext>, IShippingCostRepository
{
    private readonly ShippingCostMapper _mapper;
    
    public ShippingCostRepository(ApplicationDbContext dataContext, ShippingCostMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }

    public override async Task<IEnumerable<ShippingCostDTO>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(s => s.PackageSize)
            .Include(s => s.ShippingMethod)
            .Select(s => new ShippingCostDTO
            {
                Id = s.Id,
                PackageSize = s.PackageSize!.SizeName,
                ShippingMethod = s.ShippingMethod!.MethodName,
                CostPerUnit = s.CostPerUnit
            })
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ShippingCostEditDTO>> AllAsyncEdit()
    {
        return await RepositoryDbSet
            .Select(s => new ShippingCostEditDTO
            {
                Id = s.Id,
                PackageSizeId = s.PackageSizeId,
                ShippingMethodId = s.ShippingMethodId,
                CostPerUnit = s.CostPerUnit
            })
            .ToListAsync();
    }

    public override async Task<ShippingCostDTO?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(s => s.Id == id)
            .Include(s => s.PackageSize)
            .Include(s => s.ShippingMethod)
            .Select(s => new ShippingCostDTO
            {
                Id = s.Id,
                PackageSize = s.PackageSize!.SizeName,
                ShippingMethod = s.ShippingMethod!.MethodName,
                CostPerUnit = s.CostPerUnit
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<ShippingCostEditDTO?> FindAsyncEdit(Guid id)
    {
        return await RepositoryDbSet
            .Where(s => s.Id == id)
            .Select(s => new ShippingCostEditDTO
            {
                Id = s.Id,
                PackageSizeId = s.PackageSizeId,
                ShippingMethodId = s.ShippingMethodId,
                CostPerUnit = s.CostPerUnit
            })
            .FirstOrDefaultAsync();
    }

    public ShippingCostEditDTO Update(ShippingCostEditDTO shippingCost)
    {
        return _mapper.MapEdit(RepositoryDbSet.Update(_mapper.MapEdit(shippingCost)).Entity);
    }

    public ShippingCostEditDTO Add(ShippingCostEditDTO shippingCost)
    {
        return _mapper.MapEdit(RepositoryDbSet.Add(_mapper.MapEdit(shippingCost)).Entity);
    }
}