using AutoMapper;
using DAL.Base;
using Domain.App;
using Public.DTO.V1.ComponentAttributes;

namespace Public.DTO.Mappers;

public class ComponentAttributeMapper : 
    BaseMapper<ComponentAttributeDTO, BLL.DTO.ComponentAttribute.ComponentAttributeDTO>
{
    public ComponentAttributeMapper(IMapper mapper) : base(mapper)
    {
    }

    public BLL.DTO.ComponentAttribute.ComponentAttributeEditDTO MapCreate(ComponentAttributeCreateDTO componentAttribute)
    {
        return Mapper.Map<BLL.DTO.ComponentAttribute.ComponentAttributeEditDTO>(componentAttribute);
    }
    
    public ComponentAttributeEditDTO MapEdit(BLL.DTO.ComponentAttribute.ComponentAttributeEditDTO componentAttribute)
    {
        return Mapper.Map<ComponentAttributeEditDTO>(componentAttribute);
    }
    
    public BLL.DTO.ComponentAttribute.ComponentAttributeEditDTO MapEdit(ComponentAttributeEditDTO componentAttribute)
    {
        return Mapper.Map<BLL.DTO.ComponentAttribute.ComponentAttributeEditDTO>(componentAttribute);
    }
}