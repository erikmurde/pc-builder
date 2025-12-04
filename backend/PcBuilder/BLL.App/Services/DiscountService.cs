using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Discount;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class DiscountService : 
    BaseEntityService<DiscountDTO, DAL.DTO.Discount.DiscountDTO, IDiscountRepository>, IDiscountService
{
    private readonly IAppUOW _uow;
    private readonly DiscountMapper _mapper;
    
    public DiscountService(IAppUOW uow, DiscountMapper mapper) : 
        base(uow.DiscountRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public override DiscountDTO Update(DiscountDTO discount)
    {
        if (!EntityValidationHelper.ValidateDiscount(discount))
        {
            throw new ArgumentException("");
        }

        var result = _uow.DiscountRepository.Update(_mapper.Map(discount));
        return _mapper.Map(result);
    }

    public override DiscountDTO Add(DiscountDTO discount)
    {
        if (!EntityValidationHelper.ValidateDiscount(discount))
        {
            throw new ArgumentException("");
        }

        var result = _uow.DiscountRepository.Add(_mapper.Map(discount));
        return _mapper.Map(result);
    }
}