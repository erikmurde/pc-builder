using Public.DTO.V1.OrderPC;
using Public.DTO.V1.OrderShippingCost;
using Public.DTO.V1.Payment;

namespace Public.DTO.V1.Order;

public class OrderCreateDTO
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