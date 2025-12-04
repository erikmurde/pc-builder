using AutoMapper;
using DAL.Base;
using DAL.DTO.Discount;

namespace DAL.DTO.Mappers;

public class DiscountMapper : BaseMapper<DiscountDTO, Domain.App.Discount>
{
    public DiscountMapper(IMapper mapper) : base(mapper)
    {
    }
}