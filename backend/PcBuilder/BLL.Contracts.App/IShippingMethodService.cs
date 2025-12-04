using BLL.DTO.ShippingMethod;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IShippingMethodService : IBaseRepository<ShippingMethodDTO>
{
}