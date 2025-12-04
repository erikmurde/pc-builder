using AutoMapper;
using BLL.DTO.UserReview;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class UserReviewMapper : BaseMapper<UserReviewDTO, DAL.DTO.UserReview.UserReviewDTO>
{
    public UserReviewMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public UserReviewEditDTO MapEdit(DAL.DTO.UserReview.UserReviewEditDTO userReview)
    {
        return Mapper.Map<UserReviewEditDTO>(userReview);
    }
    
    public DAL.DTO.UserReview.UserReviewEditDTO MapEdit(UserReviewEditDTO userReview)
    {
        return Mapper.Map<DAL.DTO.UserReview.UserReviewEditDTO>(userReview);
    }
}