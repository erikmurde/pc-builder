using System.ComponentModel.DataAnnotations;
using DAL.EF.Base;
using Domain.Base;

namespace Domain.App;

public class Component : DomainEntityId
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid DiscountId { get; set; }
    public Discount? Discount { get; set; }

    public Guid ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }

    [MaxLength(128)]
    public string ComponentName { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    [DecimalRange(8, 2, ErrorMessage = "Price must be a decimal between 0 and 99,999.99")]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    
    [MaxLength(256)]
    public string? ImageSrc { get; set; }

    public ICollection<ComponentAttribute>? ComponentAttributes { get; set; }
    public ICollection<PcComponent>? PcComponents { get; set; }
}