using DAL.Contracts.App;
using DAL.DTO.Component;
using DAL.DTO.Mappers;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ComponentRepository : 
    EFBaseRepository<ComponentDTO, Component, ApplicationDbContext>, IComponentRepository
{
    private readonly ComponentMapper _mapper;
    
    public ComponentRepository(ApplicationDbContext dataContext, ComponentMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }
    
    public override async Task<IEnumerable<ComponentDTO>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c.Category)
            .Include(c => c.Discount)
            .Include(c => c.Manufacturer)
            .Select(c => new ComponentDTO
            {
                Id = c.Id,
                CategoryName = c.Category!.CategoryName,
                DiscountPercentage = c.Discount!.DiscountPercentage,
                ManufacturerName = c.Manufacturer!.ManufacturerName,
                ComponentName = c.ComponentName,
                Price = c.Price,
                Stock = c.Stock,
                ImageSrc = c.ImageSrc
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ComponentDetailsDTO>> AllAsyncMotherboard()
    {
        return await RepositoryDbSet
            .Include(c => c.Category)
            .Include(c => c.Discount)
            .Include(c => c.Manufacturer)
            .Where(c => c.Category!.CategoryName == "Motherboard")
            .Select(c => new ComponentDetailsDTO
            {
                Id = c.Id,
                CategoryName = c.Category!.CategoryName,
                DiscountName = c.Discount!.DiscountName,
                DiscountPercentage = c.Discount.DiscountPercentage,
                ManufacturerName = c.Manufacturer!.ManufacturerName,
                ComponentName = c.ComponentName,
                Description = c.Description,
                Price = c.Price,
                Stock = c.Stock,
                ImageSrc = c.ImageSrc
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ComponentDetailsDTO?> FindAsyncDetails(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Category)
            .Include(c => c.Discount)
            .Include(c => c.Manufacturer)
            .Select(c => new ComponentDetailsDTO
            {
                Id = c.Id,
                CategoryName = c.Category!.CategoryName,
                DiscountName = c.Discount!.DiscountName,
                DiscountPercentage = c.Discount.DiscountPercentage,
                ManufacturerName = c.Manufacturer!.ManufacturerName,
                ComponentName = c.ComponentName,
                Description = c.Description,
                Price = c.Price,
                Stock = c.Stock,
                ImageSrc = c.ImageSrc
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<IEnumerable<ComponentSimpleDTO>> AllAsyncSimple()
    {
        return await RepositoryDbSet
            .Include(c => c.Category)
            .Select(c => new ComponentSimpleDTO
            {
                Id = c.Id,
                CategoryName = c.Category!.CategoryName,
                ComponentName = c.ComponentName
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ComponentSimpleDTO?> FindAsyncSimple(Guid id)
    {
        return await RepositoryDbSet
            .Where(c => c.Id == id)
            .Include(c => c.Category)
            .Select(c => new ComponentSimpleDTO
            {
                Id = c.Id,
                CategoryName = c.Category!.CategoryName,
                ComponentName = c.ComponentName
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<ComponentEditDTO?> FindAsyncEdit(Guid id)
    {
        return await RepositoryDbSet
            .Where(c => c.Id == id)
            .Select(c => new ComponentEditDTO
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                DiscountId = c.DiscountId,
                ManufacturerId = c.ManufacturerId,
                ComponentName = c.ComponentName,
                Description = c.Description,
                Price = c.Price,
                Stock = c.Stock,
                ImageSrc = c.ImageSrc
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public ComponentEditDTO Update(ComponentEditDTO component)
    {
        return _mapper.MapEdit(RepositoryDbSet.Update(_mapper.MapEdit(component)).Entity);
    }

    public async Task<ComponentEditDTO> UpdateStock(ComponentEditDTO component)
    {
        var domainComponent = await RepositoryDbSet.FindAsync(component.Id);
        
        domainComponent!.Stock = component.Stock;
        
        return _mapper.MapEdit(RepositoryDbSet.Update(domainComponent).Entity);
    }

    public ComponentCreateDTO Add(ComponentCreateDTO component)
    {
        return _mapper.MapCreate(RepositoryDbSet.Update(_mapper.MapCreate(component)).Entity);
    }
}