using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class PackageSize : DomainEntityId
{
    [MaxLength(64)]
    public string SizeName { get; set; } = default!;

    public ICollection<OrderPc>? OrderPcs { get; set; }
    public ICollection<ShippingCost>? ShippingCosts { get; set; }
}