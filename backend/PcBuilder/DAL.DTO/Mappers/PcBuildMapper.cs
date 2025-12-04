using AutoMapper;
using DAL.Base;
using DAL.DTO.PcBuild;

namespace DAL.DTO.Mappers;

public class PcBuildMapper : BaseMapper<PcBuildDTO, Domain.App.PcBuild>
{
    public PcBuildMapper(IMapper mapper) : base(mapper)
    {
    }

    public Domain.App.PcBuild MapEdit(PcBuildEditDTO pcBuild)
    {
        return Mapper.Map<Domain.App.PcBuild>(pcBuild);
    }
    
    public PcBuildEditDTO MapEdit(Domain.App.PcBuild pcBuild)
    {
        return Mapper.Map<PcBuildEditDTO>(pcBuild);
    }
    
    public Domain.App.PcBuild MapCreate(PcBuildCreateDTO pcBuild)
    {
        return Mapper.Map<Domain.App.PcBuild>(pcBuild);
    }
    
    public PcBuildCreateDTO MapCreate(Domain.App.PcBuild pcBuild)
    {
        return Mapper.Map<PcBuildCreateDTO>(pcBuild);
    }
}