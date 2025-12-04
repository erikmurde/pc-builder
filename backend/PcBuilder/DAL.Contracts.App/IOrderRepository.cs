using DAL.Contracts.Base;
using DAL.DTO.Order;

namespace DAL.Contracts.App;

public interface IOrderRepository : 
    IBaseRepository<OrderDTO>, IOrderRepositoryCustom<OrderDTO, OrderDetailsDTO, OrderEditDTO>
{
    public OrderEditDTO Update(OrderUpdateDTO order);
    public OrderCreateDTO Add(OrderCreateDTO order, Guid userId);
    public Task<OrderUpdateDTO?> FindAsyncUpdate(Guid id);
}

public interface IOrderRepositoryCustom<TBase, TDetails, TEdit>
{
    public Task<IEnumerable<TBase>> AllAsync(Guid userId);
    public Task<TDetails?> FindAsyncDetails(Guid id);
    public Task<TDetails?> FindAsyncDetails(Guid id, Guid userId);
    public Task<TEdit?> FindAsyncEdit(Guid id);
    public Task<TEdit?> FindAsyncEdit(Guid id, Guid userId);
}