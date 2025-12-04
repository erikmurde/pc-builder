namespace Public.DTO.V1.PcComponent;

public class PcComponentDTO
{
    public string CategoryName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public decimal Price { get; set; }
    public string? ImageSrc { get; set; }
}