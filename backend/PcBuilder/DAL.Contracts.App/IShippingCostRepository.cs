using DAL.Contracts.Base;
using DAL.DTO.ShippingCost;

namespace DAL.Contracts.App;

public interface IShippingCostRepository : 
    IBaseRepository<ShippingCostDTO>, IShippingCostRepositoryCustom<ShippingCostEditDTO>
{
    public Task<IEnumerable<ShippingCostEditDTO>> AllAsyncEdit();
    public ShippingCostEditDTO Update(ShippingCostEditDTO shippingCost);
    public ShippingCostEditDTO Add(ShippingCostEditDTO shippingCost);
}

public interface IShippingCostRepositoryCustom<TEdit>
{
    public Task<TEdit?> FindAsyncEdit(Guid id);
}