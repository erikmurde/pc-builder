using Domain.Base;

namespace DAL.DTO.ShippingCost;

public class ShippingCostDTO : DomainEntityId
{
    public string PackageSize { get; set; } = default!;
    public string ShippingMethod { get; set; } = default!;
    public decimal CostPerUnit { get; set; }
}