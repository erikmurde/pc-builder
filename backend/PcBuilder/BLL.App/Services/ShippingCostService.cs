using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Mappers;
using BLL.DTO.ShippingCost;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class ShippingCostService : 
    BaseEntityService<ShippingCostDTO, DAL.DTO.ShippingCost.ShippingCostDTO, IShippingCostRepository>, IShippingCostService
{
    private readonly IAppUOW _uow;
    private readonly ShippingCostMapper _mapper;
    
    public ShippingCostService(IAppUOW uow, ShippingCostMapper mapper) : 
        base(uow.ShippingCostRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ShippingCostEditDTO?> FindAsyncEdit(Guid id)
    {
        var dalShippingCost = await _uow.ShippingCostRepository.FindAsyncEdit(id);
        return dalShippingCost == null ? null : _mapper.MapEdit(dalShippingCost);
    }
    
    public async Task<ShippingCostEditDTO> Update(ShippingCostEditDTO shippingCost)
    {
        var dalShippingCost = _mapper.MapEdit(shippingCost);
        
        if (await IsDuplicate(dalShippingCost))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ShippingCostRepository.Update(dalShippingCost);
        return _mapper.MapEdit(result);
    }

    public async Task<ShippingCostEditDTO> Add(ShippingCostEditDTO shippingCost)
    {
        var dalShippingCost = _mapper.MapEdit(shippingCost);
        
        if (await IsDuplicate(dalShippingCost))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ShippingCostRepository.Add(dalShippingCost);
        return _mapper.MapEdit(result);
    }
    
    private async Task<bool> IsDuplicate(DAL.DTO.ShippingCost.ShippingCostEditDTO shippingCost)
    {
        // Check that the same shipping cost is not added multiple times
            
        var shippingCosts = await _uow.ShippingCostRepository.AllAsyncEdit();
        return shippingCosts.Any(c => 
            c.Id != shippingCost.Id && // Skip own id when editing
            c.PackageSizeId == shippingCost.PackageSizeId &&
            c.ShippingMethodId == shippingCost.ShippingMethodId);
    }
}