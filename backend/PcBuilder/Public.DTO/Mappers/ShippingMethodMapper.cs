using AutoMapper;
using DAL.Base;
using Public.DTO.V1.ShippingMethod;

namespace Public.DTO.Mappers;

public class ShippingMethodMapper : BaseMapper<ShippingMethodDTO, BLL.DTO.ShippingMethod.ShippingMethodDTO>
{
    public ShippingMethodMapper(IMapper mapper) : base(mapper)
    {
    }

    public BLL.DTO.ShippingMethod.ShippingMethodDTO MapCreate(ShippingMethodCreateDTO method)
    {
        return Mapper.Map<BLL.DTO.ShippingMethod.ShippingMethodDTO>(method);
    }
}