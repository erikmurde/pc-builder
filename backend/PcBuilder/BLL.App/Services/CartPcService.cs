using System.Diagnostics.CodeAnalysis;
using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.CartPc;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class CartPcService : 
    BaseEntityService<CartPcDTO, DAL.DTO.CartPc.CartPcDTO, ICartPcRepository>, ICartPcService
{
    private readonly IAppUOW _uow;
    private readonly CartPcMapper _cartPcMapper;
    private readonly PcBuildMapper _pcBuildMapper;
    private readonly PcComponentMapper _pcComponentMapper;
    
    public CartPcService(IAppUOW uow, CartPcMapper cartPcMapper, PcBuildMapper pcBuildMapper, 
        PcComponentMapper pcComponentMapper) : 
        base(uow.CartPcRepository, cartPcMapper)
    {
        _uow = uow;
        _cartPcMapper = cartPcMapper;
        _pcBuildMapper = pcBuildMapper;
        _pcComponentMapper = pcComponentMapper;
    }
    
    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public async Task<IEnumerable<CartPcDTO>> AllAsync(Guid userId)
    {
        var dalCartPcs = await _uow.CartPcRepository.AllAsync(userId);

        return dalCartPcs
            .Select(c =>
            {
                var bllCartPc = _cartPcMapper.Map(c);
                
                var pcBuild = _pcBuildMapper.MapCart(_uow.PcBuildRepository
                    .FindAsyncCart(c.PcBuildId).Result!);
                
                // Add components to PC
                pcBuild.PcComponents = _uow.PcComponentRepository
                    .AllAsyncCart(pcBuild.Id).Result
                    .Select(p => _pcComponentMapper.MapCart(p))
                    .ToList();
                
                // Add PC to cart
                bllCartPc.PcBuild = pcBuild;
                
                return bllCartPc;
            }).ToList();
    }

    public async Task<CartPcDTO?> FindAsync(Guid id, Guid userId)
    {
        var dalCartPc = await _uow.CartPcRepository.FindAsync(id, userId);
        if (dalCartPc == null)
        {
            return null;
        }

        var cartPc = _cartPcMapper.Map(dalCartPc);
            
        var dalPcBuild = await _uow.PcBuildRepository.FindAsyncCart(dalCartPc.PcBuildId);
        if (dalPcBuild == null)
        {
            return null;
        }

        var pcBuild = _pcBuildMapper.MapCart(dalPcBuild);
        
        // Add components to PC
        pcBuild.PcComponents = (await _uow.PcComponentRepository.AllAsyncCart(pcBuild.Id))
            .Select(p => _pcComponentMapper.MapCart(p))
            .ToList();

        // Add PC to cart
        cartPc.PcBuild = pcBuild;
        return cartPc;
    }

    public async Task<CartPcEditDTO> Update(CartPcEditDTO cartPc)
    {
        if (!EntityValidationHelper.ValidateCartPc(cartPc))
        {
            throw new ArgumentException("");
        }
        if (!await CheckStock(cartPc))
        {
            throw new ArgumentException("");
        }
        
        var result = await _uow.CartPcRepository.UpdateQty(_cartPcMapper.MapEdit(cartPc));
        return _cartPcMapper.MapEdit(result);
    }

    public async Task<CartPcEditDTO> Add(CartPcEditDTO cartPc, Guid userId)
    {
        if (!EntityValidationHelper.ValidateCartPc(cartPc))
        {
            throw new ArgumentException("");
        }
        if (!await CheckStock(cartPc))
        {
            throw new ArgumentException("");
        }

        // If an existing PC is added to the cart again, only update the quantity. Only applies to prebuilts
        foreach (var dalCartPc in _uow.CartPcRepository.AllAsync(userId).Result)
        {
            if (dalCartPc.PcBuildId == cartPc.PcBuildId)
            {
                dalCartPc.Qty += cartPc.Qty;
                return _cartPcMapper.MapEdit(await _uow.CartPcRepository.UpdateQty(dalCartPc));
            }
        }

        var result = _uow.CartPcRepository.Add(_cartPcMapper.MapEdit(cartPc), userId);
        return _cartPcMapper.MapEdit(result);
    }
    
    private async Task<bool> CheckStock(CartPcEditDTO cartPc)
    {
        var pcBuild = await _uow.PcBuildRepository.FindAsyncEdit(cartPc.PcBuildId);
        if (pcBuild == null) return false;

        var category = await _uow.CategoryRepository.FindAsync(pcBuild.CategoryId);
        if (category == null) return false;
        
        switch (category.CategoryName)
        {
            case "Prebuilt PC": return pcBuild.Stock - cartPc.Qty >= 0;
            case "Custom PC":
            {
                var components = (await _uow.PcComponentRepository.AllAsyncStock(pcBuild.Id))
                    .ToList();

                // False if any components have insufficient stock
                return !components.Any(c => c.Stock - cartPc.Qty < 0);
            }
            // Template PCs should not be order-able
            default: return false;
        }
    }
}