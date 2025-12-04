namespace Public.DTO.V1.PcComponent;

public class PcComponentCartDTO
{
    public Guid ComponentId { get; set; }
    public string CategoryName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public string ComponentName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
}