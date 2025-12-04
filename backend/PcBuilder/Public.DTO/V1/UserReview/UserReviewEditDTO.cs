namespace Public.DTO.V1.UserReview;

public class UserReviewEditDTO
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string ReviewContent { get; set; } = default!;
}