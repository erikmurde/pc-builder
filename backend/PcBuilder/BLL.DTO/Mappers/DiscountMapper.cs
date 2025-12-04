using AutoMapper;
using BLL.DTO.Discount;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class DiscountMapper : BaseMapper<DiscountDTO, DAL.DTO.Discount.DiscountDTO>
{
    public DiscountMapper(IMapper mapper) : base(mapper)
    {
    }
}