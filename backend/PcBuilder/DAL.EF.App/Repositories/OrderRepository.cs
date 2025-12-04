using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.DTO.Order;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class OrderRepository :
    EFBaseRepository<OrderDTO, Order, ApplicationDbContext>, IOrderRepository
{
    private readonly OrderMapper _mapper;
    
    public OrderRepository(ApplicationDbContext dataContext, OrderMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }

    public override async Task<IEnumerable<OrderDTO>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(o => o.ApplicationUser)
            .Include(o => o.Discount)
            .Include(o => o.Status)
            .OrderByDescending(o => o.OrderPlacedAt)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                UserEmail = o.ApplicationUser!.Email!,
                Status = o.Status!.StatusName,
                DiscountPercentage = o.Discount!.DiscountPercentage,
                OrderNr = o.OrderNr,
                OrderPlacedAt = o.OrderPlacedAt,
                OrderCompletedAt = o.OrderCompletedAt,
                OrderCancelledAt = o.OrderCancelledAt,
                TotalShippingCost = o.OrderShippingCosts!.Sum(s => s.TotalCost),
                TotalPcCost = o.OrderPcs!.Sum(p => p.PricePerUnit * p.Qty)
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<OrderDTO>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Where(o => o.ApplicationUserId == userId)
            .Include(o => o.ApplicationUser)
            .Include(o => o.Discount)
            .Include(o => o.Status)
            .OrderByDescending(o => o.OrderPlacedAt)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                UserEmail = o.ApplicationUser!.Email!,
                Status = o.Status!.StatusName,
                DiscountPercentage = o.Discount!.DiscountPercentage,
                OrderNr = o.OrderNr,
                OrderPlacedAt = o.OrderPlacedAt,
                OrderCompletedAt = o.OrderCompletedAt,
                OrderCancelledAt = o.OrderCancelledAt,
                TotalShippingCost = o.OrderShippingCosts!.Sum(s => s.TotalCost),
                TotalPcCost = o.OrderPcs!.Sum(p => p.PricePerUnit * p.Qty)
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<OrderDetailsDTO?> FindAsyncDetails(Guid id)
    {
        return await RepositoryDbSet
            .Where(o => o.Id == id)
            .Include(o => o.ApplicationUser)
            .Include(o => o.Discount)
            .Include(o => o.ShippingMethod)
            .Include(o => o.Status)
            .Select(o => new OrderDetailsDTO
            {
                Id = o.Id,
                UserEmail = o.ApplicationUser!.Email!,
                Status = o.Status!.StatusName,
                DiscountPercentage = o.Discount!.DiscountPercentage,
                DiscountName = o.Discount.DiscountName,
                ShippingMethod = o.ShippingMethod!.MethodName,
                OrderNr = o.OrderNr,
                OrderPlacedAt = o.OrderPlacedAt,
                OrderCompletedAt = o.OrderCompletedAt,
                OrderCancelledAt = o.OrderCancelledAt,
                CustomerName = o.CustomerName,
                CustomerPhoneNumber = o.CustomerPhoneNumber,
                ShippingAddress = o.ShippingAddress,
                ShippingPostalCode = o.ShippingPostalCode,
                TotalShippingCost = o.OrderShippingCosts!.Sum(s => s.TotalCost),
                Comment = o.Comment
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
    
    public async Task<OrderDetailsDTO?> FindAsyncDetails(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Where(o => o.Id == id && o.ApplicationUserId == userId)
            .Include(o => o.ApplicationUser)
            .Include(o => o.Discount)
            .Include(o => o.ShippingMethod)
            .Include(o => o.Status)
            .Select(o => new OrderDetailsDTO
            {
                Id = o.Id,
                UserEmail = o.ApplicationUser!.Email!,
                Status = o.Status!.StatusName,
                DiscountPercentage = o.Discount!.DiscountPercentage,
                DiscountName = o.Discount.DiscountName,
                ShippingMethod = o.ShippingMethod!.MethodName,
                OrderNr = o.OrderNr,
                OrderPlacedAt = o.OrderPlacedAt,
                OrderCompletedAt = o.OrderCompletedAt,
                OrderCancelledAt = o.OrderCancelledAt,
                CustomerName = o.CustomerName,
                CustomerPhoneNumber = o.CustomerPhoneNumber,
                ShippingAddress = o.ShippingAddress,
                ShippingPostalCode = o.ShippingPostalCode,
                TotalShippingCost = o.OrderShippingCosts!.Sum(s => s.TotalCost),
                Comment = o.Comment
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
    
    public async Task<OrderEditDTO?> FindAsyncEdit(Guid id)
    {
        return await RepositoryDbSet
            .Where(o => o.Id == id)
            .Select(o => new OrderEditDTO
            {
                Id = o.Id,
                StatusId = o.StatusId,
                Comment = o.Comment
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
    
    public async Task<OrderEditDTO?> FindAsyncEdit(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Where(o => o.Id == id && o.ApplicationUserId == userId)
            .Select(o => new OrderEditDTO
            {
                Id = o.Id,
                StatusId = o.StatusId,
                Comment = o.Comment
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<OrderUpdateDTO?> FindAsyncUpdate(Guid id)
    {
        var domainOrder = await RepositoryDbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
        return domainOrder == null ? null : _mapper.MapUpdate(domainOrder);
    }

    public OrderEditDTO Update(OrderUpdateDTO order)
    {
        return _mapper.MapEdit(RepositoryDbSet.Update(_mapper.MapUpdate(order)).Entity);
    }
    
    public OrderCreateDTO Add(OrderCreateDTO order, Guid userId)
    {
        var domainOrder = _mapper.MapCreate(order);

        domainOrder.ApplicationUserId = userId;
        
        return _mapper.MapCreate(RepositoryDbSet.Add(domainOrder).Entity);
    }
}