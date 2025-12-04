using BLL.DTO.CartPc;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ICartPcService : IBaseRepository<CartPcDTO>, ICartPcRepositoryCustom<CartPcDTO>
{
    public Task<CartPcEditDTO> Update(CartPcEditDTO cartPc);
    public Task<CartPcEditDTO> Add(CartPcEditDTO cartPc, Guid userId);
}