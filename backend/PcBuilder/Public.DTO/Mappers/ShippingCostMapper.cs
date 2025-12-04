using AutoMapper;
using DAL.Base;
using Public.DTO.V1.ShippingCost;

namespace Public.DTO.Mappers;

public class ShippingCostMapper : BaseMapper<ShippingCostDTO, BLL.DTO.ShippingCost.ShippingCostDTO>
{
    public ShippingCostMapper(IMapper mapper) : base(mapper)
    {
    }

    public BLL.DTO.ShippingCost.ShippingCostEditDTO MapCreate(ShippingCostCreateDTO shippingCost)
    {
        return Mapper.Map<BLL.DTO.ShippingCost.ShippingCostEditDTO>(shippingCost);
    }

    public BLL.DTO.ShippingCost.ShippingCostEditDTO MapEdit(ShippingCostEditDTO shippingCost)
    {
        return Mapper.Map<BLL.DTO.ShippingCost.ShippingCostEditDTO>(shippingCost);
    }
    
    public ShippingCostEditDTO MapEdit(BLL.DTO.ShippingCost.ShippingCostEditDTO shippingCost)
    {
        return Mapper.Map<ShippingCostEditDTO>(shippingCost);
    }
}