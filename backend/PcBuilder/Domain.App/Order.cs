using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Order : DomainEntityId
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public Guid StatusId { get; set; }
    public Status? Status { get; set; }

    public Guid DiscountId { get; set; }
    public Discount? Discount { get; set; }

    public Guid ShippingMethodId { get; set; }
    public ShippingMethod? ShippingMethod { get; set; }

    [MaxLength(12)]
    public string OrderNr { get; set; } = default!;

    public DateTime OrderPlacedAt { get; set; } = DateTime.UtcNow;
    public DateTime? OrderCompletedAt { get; set; }
    public DateTime? OrderCancelledAt { get; set; }

    [MaxLength(256)]
    public string CustomerName { get; set; } = default!;
    
    [MaxLength(256)]
    public string CustomerPhoneNumber { get; set; } = default!;
    
    [MaxLength(256)]
    public string ShippingAddress { get; set; } = default!;
    
    [MaxLength(32)]
    public string ShippingPostalCode { get; set; } = default!;
    
    [MaxLength(2048)]
    public string? Comment { get; set; }
    
    public ICollection<OrderPc>? OrderPcs { get; set; }
    public ICollection<Payment>? Payments { get; set; }
    public ICollection<OrderShippingCost>? OrderShippingCosts { get; set; }
}