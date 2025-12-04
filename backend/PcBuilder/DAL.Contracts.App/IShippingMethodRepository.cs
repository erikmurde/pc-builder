using DAL.Contracts.Base;
using DAL.DTO.ShippingMethod;

namespace DAL.Contracts.App;

public interface IShippingMethodRepository : IBaseRepository<ShippingMethodDTO>
{
}