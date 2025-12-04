using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Discount : DomainEntityId
{
    [MaxLength(128)]
    public string DiscountName { get; set; } = default!;
    
    [Range(0, 100)]
    public int DiscountPercentage { get; set; }

    public ICollection<Component>? Components { get; set; }
    public ICollection<PcBuild>? PcBuilds { get; set; }
    public ICollection<Order>? Orders { get; set; }
}