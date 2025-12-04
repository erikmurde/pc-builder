using AutoMapper;
using DAL.Base;
using Public.DTO.V1.PcBuild;

namespace Public.DTO.Mappers;

public class PcBuildMapper : BaseMapper<PcBuildDTO, BLL.DTO.PcBuild.PcBuildDTO>
{
    public PcBuildMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public PcBuildDetailsDTO MapDetails(BLL.DTO.PcBuild.PcBuildDetailsDTO pcBuild)
    {
        return Mapper.Map<PcBuildDetailsDTO>(pcBuild);
    }
    
    public PcBuildStoreDTO MapStore(BLL.DTO.PcBuild.PcBuildStoreDTO pcBuild)
    {
        return Mapper.Map<PcBuildStoreDTO>(pcBuild);
    }
    
    public PcBuildEditDTO MapEdit(BLL.DTO.PcBuild.PcBuildEditDTO pcBuild)
    {
        return Mapper.Map<PcBuildEditDTO>(pcBuild);
    }
    
    public BLL.DTO.PcBuild.PcBuildEditDTO MapEdit(PcBuildEditDTO pcBuild)
    {
        return Mapper.Map<BLL.DTO.PcBuild.PcBuildEditDTO>(pcBuild);
    }
    
    public BLL.DTO.PcBuild.PcBuildCreateDTO MapCreate(PcBuildCreateDTO pcBuild)
    {
        return Mapper.Map<BLL.DTO.PcBuild.PcBuildCreateDTO>(pcBuild);
    }
}