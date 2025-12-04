using Domain.Base;

namespace BLL.DTO.ComponentAttribute;

public class ComponentAttributeDTO : DomainEntityId
{
    public string AttributeName { get; set; } = default!;
    public string AttributeValue { get; set; } = default!;
}