using Domain.Base;

namespace DAL.DTO.Attribute;

public class AttributeDTO : DomainEntityId
{
    public string AttributeName { get; set; } = default!;
}