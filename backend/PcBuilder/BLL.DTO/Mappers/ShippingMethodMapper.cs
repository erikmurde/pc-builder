using AutoMapper;
using BLL.DTO.ShippingMethod;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class ShippingMethodMapper : BaseMapper<ShippingMethodDTO, DAL.DTO.ShippingMethod.ShippingMethodDTO>
{
    public ShippingMethodMapper(IMapper mapper) : base(mapper)
    {
    }
}