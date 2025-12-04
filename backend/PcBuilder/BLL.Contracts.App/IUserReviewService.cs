using BLL.DTO.UserReview;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IUserReviewService : 
    IBaseRepository<UserReviewDTO>, IUserReviewRepositoryCustom<UserReviewDTO, UserReviewEditDTO>
{
}