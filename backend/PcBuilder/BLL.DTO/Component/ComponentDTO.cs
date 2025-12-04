using Domain.Base;

namespace BLL.DTO.Component;

public class ComponentDTO : DomainEntityId
{
    public string CategoryName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public string ManufacturerName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
}