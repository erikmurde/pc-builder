using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO.Identity;
using DAL.EF.Base;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class AppRefreshTokenRepository :
    EFBaseRepository<AppRefreshTokenDTO, AppRefreshToken, ApplicationDbContext>, IAppRefreshTokenRepository
{
    public AppRefreshTokenRepository(ApplicationDbContext dataContext, IMapper<AppRefreshTokenDTO, AppRefreshToken> mapper) : 
        base(dataContext, mapper)
    {
    }

    public async Task<IEnumerable<AppRefreshTokenDTO>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Where(t => t.AppUserId == userId)
            .Select(t => new AppRefreshTokenDTO
            {
                Id = t.Id,
                AppUserId = t.AppUserId,
                RefreshToken = t.RefreshToken,
                ExpirationTime = t.ExpirationTime,
                PreviousRefreshToken = t.PreviousRefreshToken,
                PreviousExpirationTime = t.PreviousExpirationTime
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<AppRefreshTokenDTO>> AllValidAsync(string refreshToken)
    {
        return await RepositoryDbSet
            .Where(t =>
                t.RefreshToken == refreshToken && t.ExpirationTime > DateTime.UtcNow ||
                t.PreviousRefreshToken == refreshToken && t.PreviousExpirationTime > DateTime.UtcNow)
            .Select(t => new AppRefreshTokenDTO
            {
                Id = t.Id,
                AppUserId = t.AppUserId,
                RefreshToken = t.RefreshToken,
                ExpirationTime = t.ExpirationTime,
                PreviousRefreshToken = t.PreviousRefreshToken,
                PreviousExpirationTime = t.PreviousExpirationTime
            })
            .ToListAsync();
    }
}