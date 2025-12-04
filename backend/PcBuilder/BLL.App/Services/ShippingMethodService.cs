using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Mappers;
using BLL.DTO.ShippingMethod;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class ShippingMethodService : 
    BaseEntityService<ShippingMethodDTO, DAL.DTO.ShippingMethod.ShippingMethodDTO, IShippingMethodRepository>, IShippingMethodService
{
    private readonly IAppUOW _uow;
    private readonly ShippingMethodMapper _mapper;
    public ShippingMethodService(IAppUOW uow, ShippingMethodMapper mapper) : 
        base(uow.ShippingMethodRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    
    public override ShippingMethodDTO Update(ShippingMethodDTO shippingMethod)
    {
        if (!EntityValidationHelper.ValidateShippingMethod(shippingMethod))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ShippingMethodRepository.Update(_mapper.Map(shippingMethod));
        return _mapper.Map(result);
    }

    public override ShippingMethodDTO Add(ShippingMethodDTO shippingMethod)
    {
        if (!EntityValidationHelper.ValidateShippingMethod(shippingMethod))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ShippingMethodRepository.Add(_mapper.Map(shippingMethod));
        return _mapper.Map(result);
    }
}