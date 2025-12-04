using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO.ShippingMethod;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class ShippingMethodRepository :
    EFBaseRepository<ShippingMethodDTO, ShippingMethod, ApplicationDbContext>, IShippingMethodRepository
{
    public ShippingMethodRepository(ApplicationDbContext dataContext, IMapper<ShippingMethodDTO, ShippingMethod> mapper) : 
        base(dataContext, mapper)
    {
    }
}