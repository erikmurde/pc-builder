namespace DAL.DTO.PcComponent;

public class PcComponentCartDTO
{
    public Guid ComponentId { get; set; }
    public string CategoryName { get; set; } = default!;
    public int DiscountPercentage { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
}