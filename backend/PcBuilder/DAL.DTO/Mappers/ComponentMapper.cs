using AutoMapper;
using DAL.Base;
using DAL.DTO.Component;

namespace DAL.DTO.Mappers;

public class ComponentMapper : BaseMapper<ComponentDTO, Domain.App.Component>
{
    public ComponentMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public ComponentEditDTO MapEdit(Domain.App.Component component)
    {
        return Mapper.Map<ComponentEditDTO>(component);
    }
    
    public Domain.App.Component MapEdit(ComponentEditDTO component)
    {
        return Mapper.Map<Domain.App.Component>(component);
    }
    
    public ComponentCreateDTO MapCreate(Domain.App.Component component)
    {
        return Mapper.Map<ComponentCreateDTO>(component);
    }
    
    public Domain.App.Component MapCreate(ComponentCreateDTO component)
    {
        return Mapper.Map<Domain.App.Component>(component);
    }
}