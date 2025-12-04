namespace Public.DTO.V1.ComponentAttributes;

public class ComponentAttributeEditDTO
{
    public Guid Id { get; set; }
    public Guid ComponentId { get; set; }
    public Guid AttributeId { get; set; }
    public string AttributeValue { get; set; } = default!;
}