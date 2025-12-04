using DAL.Contracts.Base;
using DAL.DTO.OrderShippingCost;

namespace DAL.Contracts.App;

public interface IOrderShippingCostRepository : IBaseRepository<OrderShippingCostDTO>
{
    public OrderShippingCostCreateDTO Add(OrderShippingCostCreateDTO orderShippingCost, Guid orderId);
}