using AutoMapper;
using BLL.DTO.Attribute;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class AttributeMapper : BaseMapper<AttributeDTO, DAL.DTO.Attribute.AttributeDTO>
{
    public AttributeMapper(IMapper mapper) : base(mapper)
    {
    }
}