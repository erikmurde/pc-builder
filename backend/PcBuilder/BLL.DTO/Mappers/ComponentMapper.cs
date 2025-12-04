using AutoMapper;
using BLL.DTO.Component;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class ComponentMapper : BaseMapper<ComponentDTO, DAL.DTO.Component.ComponentDTO>
{
    public ComponentMapper(IMapper mapper) : base(mapper)
    {
    }

    public ComponentSimpleDTO MapSimple(DAL.DTO.Component.ComponentSimpleDTO component)
    {
        return Mapper.Map<ComponentSimpleDTO>(component);
    }
    
    public ComponentDetailsDTO MapDetails(DAL.DTO.Component.ComponentDetailsDTO component)
    {
        return Mapper.Map<ComponentDetailsDTO>(component);
    }

    public ComponentEditDTO MapEdit(DAL.DTO.Component.ComponentEditDTO component)
    {
        return Mapper.Map<ComponentEditDTO>(component);
    }
    
    public DAL.DTO.Component.ComponentEditDTO MapEdit(ComponentEditDTO component)
    {
        return Mapper.Map<DAL.DTO.Component.ComponentEditDTO>(component);
    }
    
    public ComponentCreateDTO MapCreate(DAL.DTO.Component.ComponentCreateDTO component)
    {
        return Mapper.Map<ComponentCreateDTO>(component);
    }
    
    public DAL.DTO.Component.ComponentCreateDTO MapCreate(ComponentCreateDTO component)
    {
        return Mapper.Map<DAL.DTO.Component.ComponentCreateDTO>(component);
    }
}