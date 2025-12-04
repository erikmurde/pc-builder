using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Component;

namespace Public.DTO.Mappers;

public class ComponentMapper : BaseMapper<ComponentDTO, BLL.DTO.Component.ComponentDTO>
{
    public ComponentMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public ComponentDetailsDTO MapDetails(BLL.DTO.Component.ComponentDetailsDTO component)
    {
        return Mapper.Map<ComponentDetailsDTO>(component);
    }
    
    public ComponentSimpleDTO MapSimple(BLL.DTO.Component.ComponentSimpleDTO component)
    {
        return Mapper.Map<ComponentSimpleDTO>(component);
    }

    public BLL.DTO.Component.ComponentCreateDTO MapCreate(ComponentCreateDTO component)
    {
        return Mapper.Map<BLL.DTO.Component.ComponentCreateDTO>(component);
    }
    
    public ComponentEditDTO MapEdit(BLL.DTO.Component.ComponentEditDTO component)
    {
        return Mapper.Map<ComponentEditDTO>(component);
    }
    
    public BLL.DTO.Component.ComponentEditDTO MapEdit(ComponentEditDTO component)
    {
        return Mapper.Map<BLL.DTO.Component.ComponentEditDTO>(component);
    }
}