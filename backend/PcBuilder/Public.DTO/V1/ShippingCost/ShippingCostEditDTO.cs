namespace Public.DTO.V1.ShippingCost;

public class ShippingCostEditDTO
{
    public Guid Id { get; set; }
    public Guid PackageSizeId { get; set; }
    public Guid ShippingMethodId { get; set; }
    public decimal CostPerUnit { get; set; }
}