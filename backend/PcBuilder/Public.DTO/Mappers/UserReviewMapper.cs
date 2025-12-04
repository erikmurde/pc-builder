using AutoMapper;
using DAL.Base;
using Public.DTO.V1.UserReview;

namespace Public.DTO.Mappers;

public class UserReviewMapper : BaseMapper<UserReviewDTO, BLL.DTO.UserReview.UserReviewDTO>
{
    public UserReviewMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.UserReview.UserReviewEditDTO MapCreate(UserReviewCreateDTO userReview)
    {
        return Mapper.Map<BLL.DTO.UserReview.UserReviewEditDTO>(userReview);
    }

    public BLL.DTO.UserReview.UserReviewEditDTO MapEdit(UserReviewEditDTO userReview)
    {
        return Mapper.Map<BLL.DTO.UserReview.UserReviewEditDTO>(userReview);
    }
}