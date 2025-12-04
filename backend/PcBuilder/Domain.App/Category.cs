using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Category : DomainEntityId
{
    [MaxLength(64)]
    public string CategoryName { get; set; } = default!;

    public ICollection<Component>? Components { get; set; }
    public ICollection<PcBuild>? PcBuilds { get; set; }
}