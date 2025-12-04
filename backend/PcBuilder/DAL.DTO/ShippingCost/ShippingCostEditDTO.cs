using Domain.Base;

namespace DAL.DTO.ShippingCost;

public class ShippingCostEditDTO : DomainEntityId
{
    public Guid PackageSizeId { get; set; }
    public Guid ShippingMethodId { get; set; }
    public decimal CostPerUnit { get; set; }
}