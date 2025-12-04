namespace Public.DTO.V1.UserReview;

public class UserReviewCreateDTO
{
    public Guid PcBuildId { get; set; }
    public int Rating { get; set; }
    public string ReviewContent { get; set; } = default!;
}