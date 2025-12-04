using DAL.EF.Base;
using Domain.Base;

namespace Domain.App;

public class ShippingCost : DomainEntityId
{
    public Guid PackageSizeId { get; set; }
    public PackageSize? PackageSize { get; set; }

    public Guid ShippingMethodId { get; set; }
    public ShippingMethod? ShippingMethod { get; set; }

    [DecimalRange(8, 2, ErrorMessage = "Price must be a decimal between 0 and 99,999.99")]
    public decimal CostPerUnit { get; set; }

    public ICollection<OrderShippingCost>? OrderShippingCosts { get; set; }
}