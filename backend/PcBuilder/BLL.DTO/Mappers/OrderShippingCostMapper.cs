using AutoMapper;
using BLL.DTO.OrderShippingCost;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class OrderShippingCostMapper : 
    BaseMapper<OrderShippingCostCreateDTO, DAL.DTO.OrderShippingCost.OrderShippingCostCreateDTO>
{
    public OrderShippingCostMapper(IMapper mapper) : base(mapper)
    {
    }
}