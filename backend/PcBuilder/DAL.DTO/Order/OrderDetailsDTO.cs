using Domain.Base;

namespace DAL.DTO.Order;

public class OrderDetailsDTO : DomainEntityId
{
    public string UserEmail { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string DiscountName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public string ShippingMethod { get; set; } = default!;
    public string OrderNr { get; set; } = default!;
    public DateTime OrderPlacedAt { get; set; }
    public DateTime? OrderCompletedAt { get; set; }
    public DateTime? OrderCancelledAt { get; set; }
    public string CustomerName { get; set; } = default!;
    public string CustomerPhoneNumber { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShippingPostalCode { get; set; } = default!;
    public decimal TotalShippingCost { get; set; }
    public string? Comment { get; set; }
}