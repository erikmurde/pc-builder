using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Status : DomainEntityId
{
    [MaxLength(64)]
    public string StatusName { get; set; } = default!;

    [MaxLength(2048)]
    public string? Comment { get; set; }

    public ICollection<Order>? Orders { get; set; }
}