using Domain.Base;

namespace DAL.DTO.OrderShippingCost;

public class OrderShippingCostCreateDTO : DomainEntityId
{
    public Guid ShippingCostId { get; set; }
    public decimal TotalCost { get; set; }
}