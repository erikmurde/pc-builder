using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO.Discount;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class DiscountRepository :
    EFBaseRepository<DiscountDTO, Discount, ApplicationDbContext>, IDiscountRepository
{
    public DiscountRepository(ApplicationDbContext dataContext, IMapper<DiscountDTO, Discount> mapper) : 
        base(dataContext, mapper)
    {
    }
}