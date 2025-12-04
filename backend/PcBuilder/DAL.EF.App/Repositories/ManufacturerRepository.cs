using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO.Manufacturer;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class ManufacturerRepository :
    EFBaseRepository<ManufacturerDTO, Manufacturer, ApplicationDbContext>, IManufacturerRepository
{
    public ManufacturerRepository(ApplicationDbContext dataContext, IMapper<ManufacturerDTO, Manufacturer> mapper) : 
        base(dataContext, mapper)
    {
    }
}