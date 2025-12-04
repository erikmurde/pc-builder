using AutoMapper;
using BLL.DTO.Order;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class OrderMapper : BaseMapper<OrderDTO, DAL.DTO.Order.OrderDTO>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public OrderDetailsDTO MapDetails(DAL.DTO.Order.OrderDetailsDTO order)
    {
        return Mapper.Map<OrderDetailsDTO>(order);
    }
    
    public OrderEditDTO MapEdit(DAL.DTO.Order.OrderEditDTO order)
    {
        return Mapper.Map<OrderEditDTO>(order);
    }
    
    public DAL.DTO.Order.OrderEditDTO MapEdit(OrderEditDTO order)
    {
        return Mapper.Map<DAL.DTO.Order.OrderEditDTO>(order);
    }
    
    public OrderCreateDTO MapCreate(DAL.DTO.Order.OrderCreateDTO order)
    {
        return Mapper.Map<OrderCreateDTO>(order);
    }
    
    public DAL.DTO.Order.OrderCreateDTO MapCreate(OrderCreateDTO order)
    {
        return Mapper.Map<DAL.DTO.Order.OrderCreateDTO>(order);
    }
}