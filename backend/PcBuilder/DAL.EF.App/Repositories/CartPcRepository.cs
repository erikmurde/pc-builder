using DAL.Contracts.App;
using DAL.DTO.CartPc;
using DAL.DTO.Mappers;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class CartPcRepository : 
    EFBaseRepository<CartPcDTO, CartPc, ApplicationDbContext>, ICartPcRepository
{
    private readonly CartPcMapper _mapper;
    
    public CartPcRepository(ApplicationDbContext dataContext, CartPcMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CartPcDTO>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .OrderByDescending(c => c.Qty)
            .Where(c => c.ApplicationUserId == userId)
            .Select(c => new CartPcDTO
            {
                Id = c.Id,
                PcBuildId = c.PcBuildId,
                Qty = c.Qty
            }).ToListAsync();
    }

    public async Task<CartPcDTO?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .OrderByDescending(c => c.Qty)
            .Where(c => c.Id == id && c.ApplicationUserId == userId)
            .Select(c => new CartPcDTO
            {
                Id = c.Id,
                PcBuildId = c.PcBuildId,
                Qty = c.Qty
            }).FirstOrDefaultAsync();
    }

    public async Task<CartPcDTO> UpdateQty(CartPcDTO cartPc)
    {
        var domainCartPc = await RepositoryDbSet.FindAsync(cartPc.Id);

        domainCartPc!.Qty = cartPc.Qty;

        return _mapper.Map(RepositoryDbSet.Update(domainCartPc).Entity);
    }
    
    public CartPcDTO Add(CartPcDTO cartPc, Guid userId)
    {
        var domainCartPc = _mapper.Map(cartPc);
        domainCartPc.ApplicationUserId = userId;

        return _mapper.Map(RepositoryDbSet.Add(domainCartPc).Entity);
    }
}