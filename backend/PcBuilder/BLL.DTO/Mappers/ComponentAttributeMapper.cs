using AutoMapper;
using BLL.DTO.ComponentAttribute;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class ComponentAttributeMapper : 
    BaseMapper<ComponentAttributeDTO, DAL.DTO.ComponentAttribute.ComponentAttributeDTO>
{
    public ComponentAttributeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public DAL.DTO.ComponentAttribute.ComponentAttributeEditDTO MapEdit(ComponentAttributeEditDTO componentAttribute)
    {
        return Mapper.Map<DAL.DTO.ComponentAttribute.ComponentAttributeEditDTO>(componentAttribute);
    }
    
    public ComponentAttributeEditDTO MapEdit(DAL.DTO.ComponentAttribute.ComponentAttributeEditDTO componentAttribute)
    {
        return Mapper.Map<ComponentAttributeEditDTO>(componentAttribute);
    }
}