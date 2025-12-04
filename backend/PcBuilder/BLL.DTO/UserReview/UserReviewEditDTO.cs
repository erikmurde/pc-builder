using Domain.Base;

namespace BLL.DTO.UserReview;

public class UserReviewEditDTO : DomainEntityId
{
    public Guid PcBuildId { get; set; }
    public int Rating { get; set; }
    public string ReviewContent { get; set; } = default!;
}