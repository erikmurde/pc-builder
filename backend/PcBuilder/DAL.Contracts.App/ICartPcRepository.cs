using DAL.Contracts.Base;
using DAL.DTO.CartPc;

namespace DAL.Contracts.App;

public interface 
    ICartPcRepository : IBaseRepository<CartPcDTO>, ICartPcRepositoryCustom<CartPcDTO>
{
    public Task<CartPcDTO> UpdateQty(CartPcDTO cartPc);
    public CartPcDTO Add(CartPcDTO cartPc, Guid userId);
}

public interface ICartPcRepositoryCustom<TBase>
{
    public Task<IEnumerable<TBase>> AllAsync(Guid userId);
    public Task<TBase?> FindAsync(Guid id, Guid userId);
}