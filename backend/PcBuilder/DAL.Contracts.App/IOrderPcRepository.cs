using DAL.Contracts.Base;
using DAL.DTO.OrderPc;

namespace DAL.Contracts.App;

public interface IOrderPcRepository : IBaseRepository<OrderPcDTO>
{
    public Task<IEnumerable<OrderPcDTO>> AllAsync(Guid orderId);
    public OrderPcCreateDTO Add(OrderPcCreateDTO orderPc, Guid orderId);
}