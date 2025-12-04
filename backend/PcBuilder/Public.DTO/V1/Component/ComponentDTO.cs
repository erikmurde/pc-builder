namespace Public.DTO.V1.Component;

public class ComponentDTO
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public int DiscountPercentage { get; set; } = default!;
    public string ManufacturerName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
}