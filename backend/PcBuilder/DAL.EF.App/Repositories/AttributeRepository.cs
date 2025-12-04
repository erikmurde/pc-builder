using Contracts.Base;
using DAL.Contracts.App;
using DAL.EF.Base;
using Attribute = Domain.App.Attribute;
using DAL.DTO.Attribute;

namespace DAL.EF.App.Repositories;

public class AttributeRepository : 
    EFBaseRepository<AttributeDTO, Attribute, ApplicationDbContext>, IAttributeRepository
{
    public AttributeRepository(ApplicationDbContext dataContext, IMapper<AttributeDTO, Attribute> mapper) : 
        base(dataContext, mapper)
    {
    }
}