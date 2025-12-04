using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Order;

namespace Public.DTO.Mappers;

public class OrderMapper : BaseMapper<OrderDTO, BLL.DTO.Order.OrderDTO>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public OrderDetailsDTO MapDetails(BLL.DTO.Order.OrderDetailsDTO order)
    {
        return Mapper.Map<OrderDetailsDTO>(order);
    }
    
    public OrderEditDTO MapEdit(BLL.DTO.Order.OrderEditDTO order)
    {
        return Mapper.Map<OrderEditDTO>(order);
    }
    
    public BLL.DTO.Order.OrderEditDTO MapEdit(OrderEditDTO order)
    {
        return Mapper.Map<BLL.DTO.Order.OrderEditDTO>(order);
    }

    public BLL.DTO.Order.OrderCreateDTO MapCreate(OrderCreateDTO order)
    {
        return Mapper.Map<BLL.DTO.Order.OrderCreateDTO>(order);
    }
}