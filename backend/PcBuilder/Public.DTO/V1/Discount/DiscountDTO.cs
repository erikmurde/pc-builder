namespace Public.DTO.V1.Discount;

public class DiscountDTO
{
    public Guid Id { get; set; }
    public string DiscountName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
}