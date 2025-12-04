using Domain.Base;

namespace BLL.DTO.Status;

public class StatusDTO : DomainEntityId
{
    public string StatusName { get; set; } = default!;
    public string? Comment { get; set; }
}