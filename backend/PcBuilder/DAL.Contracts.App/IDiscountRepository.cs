using DAL.Contracts.Base;
using DAL.DTO.Discount;
using Domain.App;

namespace DAL.Contracts.App;

public interface IDiscountRepository : IBaseRepository<DiscountDTO>
{
}