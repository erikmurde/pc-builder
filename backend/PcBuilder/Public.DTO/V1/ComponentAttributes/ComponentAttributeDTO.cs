namespace Public.DTO.V1.ComponentAttributes;

public class ComponentAttributeDTO
{
    public Guid Id { get; set; }
    public string AttributeName { get; set; } = default!;
    public string AttributeValue { get; set; } = default!;
}