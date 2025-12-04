using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Manufacturer : DomainEntityId
{
    [MaxLength(128)]
    public string ManufacturerName { get; set; } = default!;

    public ICollection<Component>? Components { get; set; }
}