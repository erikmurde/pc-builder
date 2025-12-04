using Domain.Base;

namespace DAL.DTO.Order;

public class OrderUpdateDTO : DomainEntityId
{
    public Guid ApplicationUserId { get; set; }
    public Guid StatusId { get; set; }
    public Guid DiscountId { get; set; }
    public Guid ShippingMethodId { get; set; }
    public string OrderNr { get; set; } = default!;
    public DateTime OrderPlacedAt { get; set; } = DateTime.UtcNow;
    public DateTime? OrderCompletedAt { get; set; }
    public DateTime? OrderCancelledAt { get; set; }
    public string CustomerName { get; set; } = default!;
    public string CustomerPhoneNumber { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShippingPostalCode { get; set; } = default!;
    public string? Comment { get; set; }
}