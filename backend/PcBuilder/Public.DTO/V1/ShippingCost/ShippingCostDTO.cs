namespace Public.DTO.V1.ShippingCost;

public class ShippingCostDTO
{
    public Guid Id { get; set; }
    
    public string PackageSize { get; set; } = default!;

    public string ShippingMethod { get; set; } = default!;

    public decimal CostPerUnit { get; set; }
}