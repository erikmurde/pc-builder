using Domain.Base;

namespace Domain.App;

public class OrderShippingCost : DomainEntityId
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public Guid ShippingCostId { get; set; }
    public ShippingCost? ShippingCost { get; set; }

    public decimal TotalCost { get; set; }
}