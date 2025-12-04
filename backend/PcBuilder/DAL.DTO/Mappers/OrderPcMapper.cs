using AutoMapper;
using DAL.Base;
using DAL.DTO.OrderPc;

namespace DAL.DTO.Mappers;

public class OrderPcMapper : BaseMapper<OrderPcDTO, Domain.App.OrderPc>
{
    public OrderPcMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public OrderPcCreateDTO MapCreate(Domain.App.OrderPc orderPc)
    {
        return Mapper.Map<OrderPcCreateDTO>(orderPc);
    }
    
    public Domain.App.OrderPc MapCreate(OrderPcCreateDTO orderPc)
    {
        return Mapper.Map<Domain.App.OrderPc>(orderPc);
    }
}