using AutoMapper;
using DAL.Base;
using DAL.DTO.ComponentAttribute;

namespace DAL.DTO.Mappers;

public class ComponentAttributeMapper : BaseMapper<ComponentAttributeDTO, Domain.App.ComponentAttribute>
{
    public ComponentAttributeMapper(IMapper mapper) : base(mapper)
    {
    }

    public Domain.App.ComponentAttribute MapEdit(ComponentAttributeEditDTO componentAttribute)
    {
        return Mapper.Map<Domain.App.ComponentAttribute>(componentAttribute);
    }
    
    public ComponentAttributeEditDTO MapEdit(Domain.App.ComponentAttribute componentAttribute)
    {
        return Mapper.Map<ComponentAttributeEditDTO>(componentAttribute);
    }
}