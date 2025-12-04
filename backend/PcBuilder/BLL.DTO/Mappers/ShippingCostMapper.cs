using AutoMapper;
using BLL.DTO.ShippingCost;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class ShippingCostMapper : BaseMapper<ShippingCostDTO, DAL.DTO.ShippingCost.ShippingCostDTO>
{
    public ShippingCostMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public ShippingCostEditDTO MapEdit(DAL.DTO.ShippingCost.ShippingCostEditDTO shippingCost)
    {
        return Mapper.Map<ShippingCostEditDTO>(shippingCost);
    }
    
    public DAL.DTO.ShippingCost.ShippingCostEditDTO MapEdit(ShippingCostEditDTO shippingCost)
    {
        return Mapper.Map<DAL.DTO.ShippingCost.ShippingCostEditDTO>(shippingCost);
    }
}