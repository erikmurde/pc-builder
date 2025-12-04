using Public.DTO.V1.PcComponent;

namespace Public.DTO.V1.PcBuild;

public class PcBuildStoreDTO
{
    public Guid Id { get; set; }
    public int DiscountPercentage { get; set; }
    public string PcName { get; set; } = default!;
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
    public int NumOfReviews { get; set; }
    public int ReviewScore { get; set; }

    public ICollection<PcComponentStoreDTO> PcComponents { get; set; } = default!;
}