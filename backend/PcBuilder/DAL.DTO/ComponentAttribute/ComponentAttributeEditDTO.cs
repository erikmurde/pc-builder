using Domain.Base;

namespace DAL.DTO.ComponentAttribute;

public class ComponentAttributeEditDTO : DomainEntityId
{
    public Guid ComponentId { get; set; }
    public Guid AttributeId { get; set; }
    public string AttributeValue { get; set; } = default!;
}