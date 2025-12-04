using Domain.Base;

namespace BLL.DTO.OrderShippingCost;

public class OrderShippingCostCreateDTO : DomainEntityId
{
    public Guid ShippingCostId { get; set; }
    public decimal TotalCost { get; set; }
}