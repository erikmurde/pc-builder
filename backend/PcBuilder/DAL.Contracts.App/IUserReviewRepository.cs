using DAL.Contracts.Base;
using DAL.DTO.UserReview;

namespace DAL.Contracts.App;

public interface IUserReviewRepository : 
    IBaseRepository<UserReviewDTO>, IUserReviewRepositoryCustom<UserReviewDTO, UserReviewEditDTO>
{
}

public interface IUserReviewRepositoryCustom<TBase, TEdit>
{
    public Task<IEnumerable<TBase>> AllAsyncPcBuild(Guid pcBuildId);
    public Task<IEnumerable<TBase>> AllAsync(Guid userId);
    public Task<TBase?> FindAsync(Guid id, Guid userId);
    public Task<TEdit> Update(TEdit userReview);
    public TEdit Add(TEdit userReview, Guid userId);
}
