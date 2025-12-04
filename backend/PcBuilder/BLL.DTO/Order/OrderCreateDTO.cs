using BLL.DTO.OrderPc;
using BLL.DTO.OrderShippingCost;
using BLL.DTO.Payment;
using Domain.Base;

namespace BLL.DTO.Order;

public class OrderCreateDTO : DomainEntityId
{
    public Guid StatusId { get; set; }
    public Guid DiscountId { get; set; }
    public Guid ShippingMethodId { get; set; }
    public string OrderNr { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public string CustomerPhoneNumber { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShippingPostalCode { get; set; } = default!;
    public string? Comment { get; set; }
    
    public ICollection<OrderPcCreateDTO> OrderPcData { get; set; } = default!;
    public ICollection<PaymentCreateDTO> PaymentData { get; set; } = default!;
    public ICollection<OrderShippingCostCreateDTO> OrderShippingCostData { get; set; } = default!;
}