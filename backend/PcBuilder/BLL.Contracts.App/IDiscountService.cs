using BLL.DTO.Discount;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IDiscountService : IBaseRepository<DiscountDTO>
{
}