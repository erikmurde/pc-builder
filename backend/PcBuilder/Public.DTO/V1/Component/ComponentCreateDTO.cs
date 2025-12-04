namespace Public.DTO.V1.Component;

public class ComponentCreateDTO
{
    public Guid CategoryId { get; set; }
    public Guid DiscountId { get; set; }
    public Guid ManufacturerId { get; set; }
    public string ComponentName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
}