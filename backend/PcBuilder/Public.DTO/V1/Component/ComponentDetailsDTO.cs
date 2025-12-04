using Public.DTO.V1.ComponentAttributes;

namespace Public.DTO.V1.Component;

public class ComponentDetailsDTO
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public string DiscountName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
    public string ManufacturerName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
    public ICollection<ComponentAttributeDTO>? ComponentAttributes { get; set; }
}