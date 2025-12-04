using AutoMapper;
using DAL.Base;
using DAL.DTO.PcComponent;

namespace DAL.DTO.Mappers;

public class PcComponentMapper : BaseMapper<PcComponentDTO, Domain.App.PcComponent>
{
    public PcComponentMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public Domain.App.PcComponent MapCreate(PcComponentCreateDTO pcComponent)
    {
        return Mapper.Map<Domain.App.PcComponent>(pcComponent);
    }
    
    public PcComponentCreateDTO MapCreate(Domain.App.PcComponent pcComponent)
    {
        return Mapper.Map<PcComponentCreateDTO>(pcComponent);
    }
    
    public Domain.App.PcComponent MapEdit(PcComponentEditDTO pcComponent)
    {
        return Mapper.Map<Domain.App.PcComponent>(pcComponent);
    }
    
    public PcComponentEditDTO MapEdit(Domain.App.PcComponent pcComponent)
    {
        return Mapper.Map<PcComponentEditDTO>(pcComponent);
    }
}