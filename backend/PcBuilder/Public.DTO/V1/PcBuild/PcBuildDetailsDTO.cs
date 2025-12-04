using Public.DTO.V1.PcComponent;
using Public.DTO.V1.UserReview;

namespace Public.DTO.V1.PcBuild;

public class PcBuildDetailsDTO
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public string DiscountName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public string PcName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
    
    public ICollection<PcComponentDTO> PcComponents { get; set; } = default!;
    public ICollection<UserReviewDTO> UserReviews { get; set; } = default!;
}