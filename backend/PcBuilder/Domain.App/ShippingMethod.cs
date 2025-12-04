using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class ShippingMethod : DomainEntityId
{
    [MaxLength(64)]
    public string MethodName { get; set; } = default!;
    [MaxLength(64)]
    public string ShippingTime { get; set; } = default!;

    public ICollection<ShippingCost>? ShippingCosts { get; set; }
    public ICollection<Order>? Orders { get; set; }
}