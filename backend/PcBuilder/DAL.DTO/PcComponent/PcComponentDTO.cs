namespace DAL.DTO.PcComponent;

public class PcComponentDTO
{
    public string CategoryName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
    public decimal Price { get; set; }
    public int DiscountPercentage { get; set; }
    public string? ImageSrc { get; set; }
}