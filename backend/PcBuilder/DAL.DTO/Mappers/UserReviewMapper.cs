using AutoMapper;
using DAL.Base;
using DAL.DTO.UserReview;

namespace DAL.DTO.Mappers;

public class UserReviewMapper : BaseMapper<UserReviewDTO, Domain.App.UserReview>
{
    public UserReviewMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public UserReviewEditDTO MapEdit(Domain.App.UserReview userReview)
    {
        return Mapper.Map<UserReviewEditDTO>(userReview);
    }
    
    public Domain.App.UserReview MapEdit(UserReviewEditDTO userReview)
    {
        return Mapper.Map<Domain.App.UserReview>(userReview);
    }
}