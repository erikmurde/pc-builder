using DAL.Contracts.Base;
using DAL.DTO.PcBuild;

namespace DAL.Contracts.App;

public interface IPcBuildRepository : 
    IBaseRepository<PcBuildDTO>, IPcBuildRepositoryCustom<PcBuildDetailsDTO, PcBuildEditDTO, PcBuildStoreDTO>
{
    public Task<PcBuildCartDTO?> FindAsyncCart(Guid id);
    public PcBuildEditDTO Update(PcBuildEditDTO pcBuild);
    public PcBuildCreateDTO Add(PcBuildCreateDTO pcBuild);
}

public interface IPcBuildRepositoryCustom<TDetails, TEdit, TStore>
{
    public Task<IEnumerable<TStore>> AllAsyncStore();
    public Task<TDetails?> FindAsyncDetails(Guid id);
    public Task<TEdit?> FindAsyncEdit(Guid id);
}