using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class ComponentAttribute : DomainEntityId
{
    public Guid ComponentId { get; set; }
    public Component? Component { get; set; }

    public Guid AttributeId { get; set; }
    public Attribute? Attribute { get; set; }

    [MaxLength(128)]
    public string AttributeValue { get; set; } = default!;
}