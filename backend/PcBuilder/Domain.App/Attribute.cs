using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Attribute : DomainEntityId
{
    [MaxLength(64)]
    public string AttributeName { get; set; } = default!;

    public ICollection<ComponentAttribute>? ComponentAttributes { get; set; }
}