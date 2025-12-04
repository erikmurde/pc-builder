using Domain.Base;

namespace DAL.DTO.Status;

public class StatusDTO : DomainEntityId
{
    public string StatusName { get; set; } = default!;

    public string? Comment { get; set; }
}