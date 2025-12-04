using Domain.Base;

namespace DAL.DTO.UserReview;

public class UserReviewDTO : DomainEntityId
{
    public Guid PcBuildId { get; set; }
    public string UserEmail { get; set; } = default!;
    public int Rating { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; } = default!;
}