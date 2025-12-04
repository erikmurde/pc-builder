using BLL.DTO.ShippingCost;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IShippingCostService : 
    IBaseRepository<ShippingCostDTO>, IShippingCostRepositoryCustom<ShippingCostEditDTO>
{
    public Task<ShippingCostEditDTO> Update(ShippingCostEditDTO shippingCost);
    public Task<ShippingCostEditDTO> Add(ShippingCostEditDTO shippingCost);
}