using AutoMapper;
using DAL.Base;
using DAL.DTO.ShippingMethod;

namespace DAL.DTO.Mappers;

public class ShippingMethodMapper : BaseMapper<ShippingMethodDTO, Domain.App.ShippingMethod>
{
    public ShippingMethodMapper(IMapper mapper) : base(mapper)
    {
    }
}