using DAL.Contracts.Base;
using DAL.DTO.Attribute;
using Attribute = Domain.App.Attribute;

namespace DAL.Contracts.App;

public interface IAttributeRepository : IBaseRepository<AttributeDTO>
{
}