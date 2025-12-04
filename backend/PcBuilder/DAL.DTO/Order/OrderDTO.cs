using Domain.Base;

namespace DAL.DTO.Order;

public class OrderDTO : DomainEntityId
{
    public string UserEmail { get; set; } = default!;
    public string Status { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public string OrderNr { get; set; } = default!;
    public DateTime OrderPlacedAt { get; set; }
    public DateTime? OrderCompletedAt { get; set; }
    public DateTime? OrderCancelledAt { get; set; }
    public decimal TotalShippingCost { get; set; }
    public decimal TotalPcCost { get; set; }
}