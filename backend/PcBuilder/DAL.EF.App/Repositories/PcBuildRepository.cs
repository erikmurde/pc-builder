using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using DAL.DTO.PcBuild;

namespace DAL.EF.App.Repositories;

public class PcBuildRepository : 
    EFBaseRepository<PcBuildDTO, PcBuild, ApplicationDbContext>, IPcBuildRepository
{
    private readonly PcBuildMapper _mapper;
    
    public PcBuildRepository(ApplicationDbContext dataContext, PcBuildMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }

    public override async Task<IEnumerable<PcBuildDTO>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(p => p.Category)
            .Include(p => p.Discount)
            .Include(p => p.PcComponents!)
                .ThenInclude(c => c.Component)
                .ThenInclude(c => c!.Discount)
            .Select(p => new PcBuildDTO
            {
                Id = p.Id,
                CategoryName = p.Category!.CategoryName,
                DiscountPercentage = p.Discount!.DiscountPercentage,
                PcName = p.PcName,
                Description = p.Description,
                Stock = p.Stock,
                ImageSrc = p.ImageSrc,
                Cost = p.PcComponents!.Sum(c => 
                    c.Component!.Price * (1 - c.Component.Discount!.DiscountPercentage / 100)) 
                       * (1 - p.Discount!.DiscountPercentage / 100)
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<PcBuildStoreDTO>> AllAsyncStore()
    {
        return await RepositoryDbSet
            .Include(p => p.Discount)
            .Include(p => p.Category)
            .Where(p => p.Category!.CategoryName == "Prebuilt PC")
            .Select(p => new PcBuildStoreDTO
            {
                Id = p.Id,
                DiscountPercentage = p.Discount!.DiscountPercentage,
                PcName = p.PcName,
                Stock = p.Stock,
                ImageSrc = p.ImageSrc,
                NumOfReviews = p.UserReviews!.Count,
                ReviewScore = p.UserReviews!.Count == 0 
                    ? 0
                    : p.UserReviews!.Sum(r => r.Rating) / p.UserReviews.Count
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<PcBuildDTO?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(c => c.Id == id)
            .Include(p => p.Discount)
            .Include(p => p.Category)
            .Select(p => new PcBuildDTO
            {
                Id = p.Id,
                CategoryName =p.Category!.CategoryName,
                DiscountPercentage = p.Discount!.DiscountPercentage,
                PcName = p.PcName,
                Description = p.Description,
                Stock = p.Stock,
                ImageSrc = p.ImageSrc,
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<PcBuildDetailsDTO?> FindAsyncDetails(Guid id)
    {
        return await RepositoryDbSet
            .Where(p => p.Id == id)
            .Include(p => p.Category)
            .Include(p => p.Discount)
            .Select(p => new PcBuildDetailsDTO
            {
                Id = p.Id,
                CategoryName = p.Category!.CategoryName,
                DiscountName = p.Discount!.DiscountName,
                DiscountPercentage = p.Discount.DiscountPercentage,
                PcName = p.PcName,
                Description = p.Description,
                Stock = p.Stock,
                ImageSrc = p.ImageSrc   
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
    
    public async Task<PcBuildEditDTO?> FindAsyncEdit(Guid id)
    {
        return await RepositoryDbSet
            .Where(p => p.Id == id)
            .Select(p => new PcBuildEditDTO
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                DiscountId = p.DiscountId,
                PcName = p.PcName,
                Description = p.Description,
                Stock = p.Stock,
                ImageSrc = p.ImageSrc
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<PcBuildCartDTO?> FindAsyncCart(Guid id)
    {
        return await RepositoryDbSet
            .Where(p => p.Id == id)
            .Include(p => p.Discount)
            .Include(p => p.Category)
            .Select(p => new PcBuildCartDTO
            {
                Id = p.Id,
                DiscountPercentage = p.Discount!.DiscountPercentage,
                PcName = p.PcName,
                ImageSrc = p.ImageSrc,
                IsCustom = p.Category!.CategoryName == "Custom PC",
                Stock = p.Stock
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
   
    }

    public PcBuildEditDTO Update(PcBuildEditDTO pcBuild)
    {
        return _mapper.MapEdit(RepositoryDbSet.Update(_mapper.MapEdit(pcBuild)).Entity);
    }

    public PcBuildCreateDTO Add(PcBuildCreateDTO pcBuild)
    {
        return _mapper.MapCreate(RepositoryDbSet.Add(_mapper.MapCreate(pcBuild)).Entity);
    }
}