namespace Public.DTO.V1.UserReview;

public class UserReviewDTO
{
    public Guid Id { get; set; }
    public Guid PcBuildId { get; set; }
    public string UserEmail { get; set; } = default!;
    public int Rating { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; } = default!;
}