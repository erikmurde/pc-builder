namespace Public.DTO.V1.ComponentAttributes;

public class ComponentAttributeCreateDTO
{
    public Guid ComponentId { get; set; }
    public Guid AttributeId { get; set; }
    public string AttributeValue { get; set; } = default!;
}