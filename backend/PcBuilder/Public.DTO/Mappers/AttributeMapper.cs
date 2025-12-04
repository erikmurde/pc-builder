using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Attribute;

namespace Public.DTO.Mappers;

public class AttributeMapper : BaseMapper<AttributeDTO, BLL.DTO.Attribute.AttributeDTO>
{
    public AttributeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.Attribute.AttributeDTO MapCreate(AttributeCreateDTO attribute)
    {
        return Mapper.Map<BLL.DTO.Attribute.AttributeDTO>(attribute);
    }
}