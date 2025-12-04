namespace Public.DTO.V1.Discount;

public class DiscountCreateDTO
{
    public string DiscountName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
}