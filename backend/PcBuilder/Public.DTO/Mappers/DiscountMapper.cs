using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Discount;
namespace Public.DTO.Mappers;

public class DiscountMapper : BaseMapper<DiscountDTO, BLL.DTO.Discount.DiscountDTO>
{
    public DiscountMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.Discount.DiscountDTO MapCreate(DiscountCreateDTO discount)
    {
        return Mapper.Map<BLL.DTO.Discount.DiscountDTO>(discount);
    }
}