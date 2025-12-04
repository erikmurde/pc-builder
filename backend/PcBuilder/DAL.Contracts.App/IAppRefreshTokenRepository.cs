using DAL.Contracts.Base;
using DAL.DTO.Identity;
namespace DAL.Contracts.App;

public interface IAppRefreshTokenRepository : 
    IBaseRepository<AppRefreshTokenDTO>, IAppRefreshTokenRepositoryCustom<AppRefreshTokenDTO>
{
}

public interface IAppRefreshTokenRepositoryCustom<TEntity>
{
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    public Task<IEnumerable<TEntity>> AllValidAsync(string refreshToken);
}