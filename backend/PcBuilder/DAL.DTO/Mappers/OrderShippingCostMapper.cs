using AutoMapper;
using DAL.Base;
using DAL.DTO.OrderShippingCost;

namespace DAL.DTO.Mappers;

public class OrderShippingCostMapper : BaseMapper<OrderShippingCostDTO, Domain.App.OrderShippingCost>
{
    public OrderShippingCostMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public OrderShippingCostCreateDTO MapCreate(Domain.App.OrderShippingCost orderShippingCost)
    {
        return Mapper.Map<OrderShippingCostCreateDTO>(orderShippingCost);
    }
    
    public Domain.App.OrderShippingCost MapCreate(OrderShippingCostCreateDTO orderShippingCost)
    {
        return Mapper.Map<Domain.App.OrderShippingCost>(orderShippingCost);
    }
}