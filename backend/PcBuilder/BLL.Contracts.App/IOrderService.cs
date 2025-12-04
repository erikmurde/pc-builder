using BLL.DTO.Order;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IOrderService : 
    IBaseRepository<OrderDTO>, IOrderRepositoryCustom<OrderDTO, OrderDetailsDTO, OrderEditDTO>
{
    public Task<OrderEditDTO> Update(OrderEditDTO order, bool userIsAdmin);
    public Task<OrderCreateDTO> Add(OrderCreateDTO order, Guid userId);    
}