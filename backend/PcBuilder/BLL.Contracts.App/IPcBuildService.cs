using BLL.DTO.PcBuild;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IPcBuildService : 
    IBaseRepository<PcBuildDTO>, IPcBuildRepositoryCustom<PcBuildDetailsDTO, PcBuildEditDTO, PcBuildStoreDTO>
{
    public Task<PcBuildEditDTO> Update(PcBuildEditDTO pcBuild);
    public Task<PcBuildCreateDTO> Add(PcBuildCreateDTO pcBuild);
}