using DTO.Base;

namespace BLL.DTO.Component;

public class ComponentEditDTO : ComponentBaseDTO
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid DiscountId { get; set; }
    public Guid ManufacturerId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageSrc { get; set; }
}