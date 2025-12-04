namespace DAL.DTO.PcBuild;

public class PcBuildStoreDTO
{
    public Guid Id { get; set; }
    public int DiscountPercentage { get; set; }
    public string PcName { get; set; } = default!;
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
    public int NumOfReviews { get; set; }
    public int ReviewScore { get; set; }
}