using Domain.Base;

namespace BLL.DTO.Attribute;

public class AttributeDTO : DomainEntityId
{
    public string AttributeName { get; set; } = default!;
}