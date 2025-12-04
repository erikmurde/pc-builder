namespace Public.DTO.V1.PcBuild;

public class PcBuildDTO
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public string PcName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
    public decimal Cost { get; set; }
}