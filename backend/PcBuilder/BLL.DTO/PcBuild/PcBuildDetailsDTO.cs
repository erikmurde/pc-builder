using BLL.DTO.PcComponent;
using BLL.DTO.UserReview;
using Domain.Base;

namespace BLL.DTO.PcBuild;

public class PcBuildDetailsDTO : DomainEntityId
{
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