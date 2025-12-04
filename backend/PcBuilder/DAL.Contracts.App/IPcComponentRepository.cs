using DAL.Contracts.Base;
using DAL.DTO.PcComponent;

namespace DAL.Contracts.App;

public interface IPcComponentRepository : IBaseRepository<PcComponentDTO>
{
    public Task<IEnumerable<PcComponentDTO>> AllAsync(Guid pcBuildId);
    public Task<IEnumerable<PcComponentSimpleDTO>> AllAsyncSimple(Guid pcBuildId);
    public Task<IEnumerable<PcComponentStockDTO>> AllAsyncStock(Guid pcBuildId);
    public Task<IEnumerable<PcComponentEditDTO>> AllAsyncEdit(Guid pcBuildId);
    public Task<IEnumerable<PcComponentCartDTO>> AllAsyncCart(Guid pcBuildId);
    public Task<IEnumerable<PcComponentStoreDTO>> AllAsyncStore(Guid pcBuildId);
    public PcComponentCreateDTO Add(PcComponentCreateDTO pcComponent);
    public PcComponentEditDTO Update(PcComponentEditDTO pcComponent);
}