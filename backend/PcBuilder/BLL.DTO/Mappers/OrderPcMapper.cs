using AutoMapper;
using BLL.DTO.OrderPc;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class OrderPcMapper : BaseMapper<OrderPcDTO, DAL.DTO.OrderPc.OrderPcDTO>
{
    public OrderPcMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public OrderPcCreateDTO MapCreate(DAL.DTO.OrderPc.OrderPcCreateDTO orderPc)
    {
        return Mapper.Map<OrderPcCreateDTO>(orderPc);
    }
    
    public DAL.DTO.OrderPc.OrderPcCreateDTO MapCreate(OrderPcCreateDTO orderPc)
    {
        return Mapper.Map<DAL.DTO.OrderPc.OrderPcCreateDTO>(orderPc);
    }
}