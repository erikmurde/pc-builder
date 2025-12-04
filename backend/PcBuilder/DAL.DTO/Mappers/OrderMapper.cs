using AutoMapper;
using DAL.Base;
using DAL.DTO.Order;

namespace DAL.DTO.Mappers;

public class OrderMapper : BaseMapper<OrderDTO, Domain.App.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public OrderEditDTO MapEdit(Domain.App.Order order)
    {
        return Mapper.Map<OrderEditDTO>(order);
    }

    public OrderCreateDTO MapCreate(Domain.App.Order order)
    {
        return Mapper.Map<OrderCreateDTO>(order);
    }
    
    public Domain.App.Order MapCreate(OrderCreateDTO order)
    {
        return Mapper.Map<Domain.App.Order>(order);
    }
    
    public OrderUpdateDTO MapUpdate(Domain.App.Order order)
    {
        return Mapper.Map<OrderUpdateDTO>(order);
    }
    
    public Domain.App.Order MapUpdate(OrderUpdateDTO order)
    {
        return Mapper.Map<Domain.App.Order>(order);
    }
}