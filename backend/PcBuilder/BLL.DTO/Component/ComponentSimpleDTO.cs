using Domain.Base;

namespace BLL.DTO.Component;

public class ComponentSimpleDTO : DomainEntityId
{
    public string CategoryName { get; set; } = default!;
    public string ComponentName { get; set; } = default!;
}