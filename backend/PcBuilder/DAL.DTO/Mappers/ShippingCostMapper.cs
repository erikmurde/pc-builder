using AutoMapper;
using DAL.Base;
using DAL.DTO.ShippingCost;

namespace DAL.DTO.Mappers;

public class ShippingCostMapper : BaseMapper<ShippingCostDTO, Domain.App.ShippingCost>
{
    public ShippingCostMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public ShippingCostEditDTO MapEdit(Domain.App.ShippingCost shippingCost)
    {
        return Mapper.Map<ShippingCostEditDTO>(shippingCost);
    }
    
    public Domain.App.ShippingCost MapEdit(ShippingCostEditDTO shippingCost)
    {
        return Mapper.Map<Domain.App.ShippingCost>(shippingCost);
    }
}