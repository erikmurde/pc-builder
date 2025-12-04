using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.DTO.UserReview;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class UserReviewRepository : 
    EFBaseRepository<UserReviewDTO, UserReview, ApplicationDbContext>, IUserReviewRepository
{
    private readonly UserReviewMapper _mapper;
    
    public UserReviewRepository(ApplicationDbContext dataContext, UserReviewMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReviewDTO>> AllAsyncPcBuild(Guid pcBuildId)
    {
        return await RepositoryDbSet
            .Where(u => u.PcBuildId == pcBuildId)
            .Include(u => u.ApplicationUser)
            .OrderByDescending(u => u.ReviewDate)
            .Select(r => new UserReviewDTO
            {
                Id = r.Id,
                PcBuildId = r.PcBuildId,
                UserEmail = r.ApplicationUser!.Email!,
                Rating = r.Rating,
                ReviewDate = r.ReviewDate,
                ReviewContent = r.ReviewContent
            })
            .ToListAsync();
    }
    
    public async Task<IEnumerable<UserReviewDTO>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Where(r => r.ApplicationUserId == userId)
            .Include(u => u.ApplicationUser)
            .OrderByDescending(u => u.ReviewDate)
            .Select(r => new UserReviewDTO
            {
                Id = r.Id,
                PcBuildId = r.PcBuildId,
                UserEmail = r.ApplicationUser!.Email!,
                Rating = r.Rating,
                ReviewDate = r.ReviewDate,
                ReviewContent = r.ReviewContent
            })
            .ToListAsync();
    }
    
    public override async Task<UserReviewDTO?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(r => r.Id == id)
            .Include(u => u.ApplicationUser)
            .Select(r => new UserReviewDTO
            {
                Id = r.Id,
                PcBuildId = r.PcBuildId,
                UserEmail = r.ApplicationUser!.Email!,
                Rating = r.Rating,
                ReviewDate = r.ReviewDate,
                ReviewContent = r.ReviewContent
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<UserReviewDTO?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Where(r => r.Id == id && r.ApplicationUserId == userId)
            .Include(u => u.ApplicationUser)
            .Select(r => new UserReviewDTO
            {
                Id = r.Id,
                PcBuildId = r.PcBuildId,
                UserEmail = r.ApplicationUser!.Email!,
                Rating = r.Rating,
                ReviewDate = r.ReviewDate,
                ReviewContent = r.ReviewContent
            })
            .FirstOrDefaultAsync();
    }

    public async Task<UserReviewEditDTO> Update(UserReviewEditDTO userReview)
    {
        var domainUserReview = await RepositoryDbSet.FindAsync(userReview.Id);

        domainUserReview!.Rating = userReview.Rating;
        domainUserReview.ReviewContent = userReview.ReviewContent;
        domainUserReview.ReviewDate = DateTime.UtcNow;

        return _mapper.MapEdit(RepositoryDbSet.Update(domainUserReview).Entity);
    }

    public UserReviewEditDTO Add(UserReviewEditDTO userReview, Guid userId)
    {
        var domainUserReview = _mapper.MapEdit(userReview);
        domainUserReview.ApplicationUserId = userId;
        
        return _mapper.MapEdit(RepositoryDbSet.Add(domainUserReview).Entity);
    }
}