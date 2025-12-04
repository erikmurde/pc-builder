using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Mappers;
using BLL.DTO.Order;
using BLL.DTO.OrderPc;
using DAL.Contracts.App;
using DAL.DTO.PcBuild;
using DAL.EF.App.Seeding;
using DAL.EF.App.Seeding.Names;
using Helpers.Base;

namespace BLL.App.Services;

public class OrderService :
    BaseEntityService<OrderDTO, DAL.DTO.Order.OrderDTO, IOrderRepository>, IOrderService
{
    private readonly IAppUOW _uow;
    private readonly OrderMapper _orderMapper;
    private readonly OrderPcMapper _orderPcMapper;
    private readonly PaymentMapper _paymentMapper;
    private readonly PcBuildMapper _pcBuildMapper;
    private readonly OrderShippingCostMapper _shippingCostMapper;

    public OrderService(IAppUOW uow, OrderMapper orderMapper, OrderPcMapper orderPcMapper, 
        PaymentMapper paymentMapper, PcBuildMapper pcBuildMapper, OrderShippingCostMapper orderShippingCostMapper) :
        base(uow.OrderRepository, orderMapper)
    {
        _uow = uow;
        _orderMapper = orderMapper;
        _orderPcMapper = orderPcMapper;
        _paymentMapper = paymentMapper;
        _pcBuildMapper = pcBuildMapper;
        _shippingCostMapper = orderShippingCostMapper;
    }

    public async Task<IEnumerable<OrderDTO>> AllAsync(Guid userId)
    {
        return (await _uow.OrderRepository.AllAsync(userId))
            .Select(o => _orderMapper.Map(o));
    }

    public async Task<OrderDetailsDTO?> FindAsyncDetails(Guid id)
    {
        var dalOrder = await _uow.OrderRepository.FindAsyncDetails(id);
        if (dalOrder == null)
        {
            return null;
        }

        return await GetOrderDetails(dalOrder);
    }

    public async Task<OrderDetailsDTO?> FindAsyncDetails(Guid id, Guid userId)
    {
        var dalOrder = await _uow.OrderRepository.FindAsyncDetails(id, userId);
        if (dalOrder == null)
        {
            return null;
        }

        return await GetOrderDetails(dalOrder);
    }

    public async Task<OrderEditDTO?> FindAsyncEdit(Guid id)
    {
        var dalOrder = await _uow.OrderRepository.FindAsyncEdit(id);
        return dalOrder == null ? null : _orderMapper.MapEdit(dalOrder);
    }
    
    public async Task<OrderEditDTO?> FindAsyncEdit(Guid id, Guid userId)
    {
        var dalOrder = await _uow.OrderRepository.FindAsyncEdit(id, userId);
        return dalOrder == null ? null : _orderMapper.MapEdit(dalOrder);
    }

    public async Task<OrderEditDTO> Update(OrderEditDTO order, bool userIsAdmin)
    {
        var statusName = await _uow.StatusRepository.GetNameById(order.StatusId);
        
        // Non-admin user can only cancel orders
        if (!userIsAdmin && statusName != "Cancelled")
        {
            throw new ArgumentException("");
        }
        if (!EntityValidationHelper.ValidateOrderEdit(order))
        {
            throw new ArgumentException("");
        }

        var dalOrder = await _uow.OrderRepository.FindAsyncUpdate(order.Id);
        if (dalOrder == null)
        {
            throw new ArgumentException("");
        }

        var dalOrderStatus = await _uow.StatusRepository.GetNameById(dalOrder.StatusId);

        // Non-admin user should not edit completed order
        if (!userIsAdmin && dalOrderStatus == "Completed")
        {
            throw new ArgumentException("");
        }
        
        dalOrder.StatusId = order.StatusId;
        dalOrder.Comment = order.Comment;

        switch (statusName)
        {
            case "Cancelled":
                dalOrder.OrderCompletedAt = null;
                dalOrder.OrderCancelledAt = DateTime.UtcNow;
                break;
            case "Completed":
                dalOrder.OrderCompletedAt = DateTime.UtcNow;
                dalOrder.OrderCancelledAt = null;
                break;
            default:
                dalOrder.OrderCancelledAt = null;
                dalOrder.OrderCompletedAt = null;
                break;
        }        
        
        var result = _uow.OrderRepository.Update(dalOrder);
        return _orderMapper.MapEdit(result);
    }

    public async Task<OrderCreateDTO> Add(OrderCreateDTO order, Guid userId)
    {
        if (!EntityValidationHelper.ValidateOrderCreate(order))
        {
            throw new ArgumentException("");
        }
        if (!await CheckAndUpdateStock(order))
        {
            throw new ArgumentException("");
        }

        var dalOrder = _orderMapper.MapCreate(order);

        // Add empty order
        dalOrder = _uow.OrderRepository.Add(dalOrder, userId);
        await _uow.SaveChangesAsync();
        
        // Add OrderPcs to order
        foreach (var orderPc in order.OrderPcData)
        {
            var dalOrderPc = _orderPcMapper.MapCreate(orderPc);
            _uow.OrderPcRepository.Add(dalOrderPc, dalOrder.Id);
        }

        // Add OrderShippingCosts to order
        foreach (var shippingCost in order.OrderShippingCostData)
        {
            var dalShippingCost = _shippingCostMapper.Map(shippingCost);
            _uow.OrderShippingCostRepository.Add(dalShippingCost, dalOrder.Id);
        }

        // Add Payments to order
        foreach (var payment in order.PaymentData)
        {
            var dalPayment = _paymentMapper.MapCreate(payment);
            _uow.PaymentRepository.Add(dalPayment, userId, dalOrder.Id);
        }

        return _orderMapper.MapCreate(dalOrder);
    }

    private async Task<OrderDetailsDTO> GetOrderDetails(DAL.DTO.Order.OrderDetailsDTO dalOrder)
    {
        var order = _orderMapper.MapDetails(dalOrder);
        
        var orderPcs = (await _uow.OrderPcRepository.AllAsync(order.Id))
            .Select(o =>
            {
                var orderPc = _orderPcMapper.Map(o);
                var pcBuild = _uow.PcBuildRepository.FindAsync(o.PcBuildId).Result;
                orderPc.PcBuild = _pcBuildMapper.Map(pcBuild);
                return orderPc;
            })
            .ToList();
        
        var payments = (await _uow.PaymentRepository.AllAsyncSimple(order.Id))
            .Select(p => _paymentMapper.MapSimple(p))
            .ToList();

        order.OrderPcs = orderPcs;
        order.Payments = payments;

        return order;
    }
    
    private async Task<bool> CheckAndUpdateStock(OrderCreateDTO order)
    {
        // Perform a stock check on all PCs in the order and update the stock values
        // I tried, but it is still a very messy solution :(
        
        foreach (var orderPc in order.OrderPcData)
        {
            var pcBuild = await _uow.PcBuildRepository.FindAsyncEdit(orderPc.PcBuildId);
            if (pcBuild == null) return false;

            var category = await _uow.CategoryRepository.FindAsync(pcBuild.CategoryId);
            if (category == null) return false;
            
            switch (category.CategoryName)
            {
                case CategoryNames.PrebuiltPc when pcBuild.Stock - orderPc.Qty < 0: return false; // Insufficient stock
                case CategoryNames.PrebuiltPc:
                    pcBuild.Stock -= orderPc.Qty;
                    _uow.PcBuildRepository.Update(pcBuild);
                    break;
                case CategoryNames.CustomPc:
                {
                    if (!await UpdateComponentStock(orderPc, pcBuild)) return false;
                    break;
                }
                // Template PCs should not be order-able
                default: return false;
            }
        }
        return true;
    }

    private async Task<bool> UpdateComponentStock(OrderPcCreateDTO orderPc, PcBuildEditDTO pcBuild)
    {
        // Check and update stock value of every component in the PC 
        foreach (var component in (await _uow.PcComponentRepository.AllAsyncStock(pcBuild.Id)).ToList())
        {
            if (component.Stock - orderPc.Qty < 0) return false; // Insufficient stock
            
            var dalComponent = await _uow.ComponentRepository.FindAsyncEdit(component.ComponentId);
            if (dalComponent == null) return false;

            dalComponent.Stock -= orderPc.Qty;
            
            await _uow.ComponentRepository.UpdateStock(dalComponent);
        }
        return true;
    }
}