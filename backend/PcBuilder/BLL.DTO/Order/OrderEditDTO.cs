using Domain.Base;

namespace BLL.DTO.Order;

public class OrderEditDTO : DomainEntityId
{
    public Guid StatusId { get; set; }
    public string? Comment { get; set; }
}