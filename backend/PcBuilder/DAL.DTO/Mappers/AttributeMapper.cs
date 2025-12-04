using AutoMapper;
using DAL.Base;
using DAL.DTO.Attribute;

namespace DAL.DTO.Mappers;

public class AttributeMapper : BaseMapper<AttributeDTO, Domain.App.Attribute>
{
    public AttributeMapper(IMapper mapper) : base(mapper)
    {
    }
}