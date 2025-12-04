namespace BLL.DTO.PcComponent;

public class PcComponentStoreDTO
{
    public string CategoryName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
    public int DiscountPercentage { get; set; } = default!;
    public decimal Price { get; set; }
}