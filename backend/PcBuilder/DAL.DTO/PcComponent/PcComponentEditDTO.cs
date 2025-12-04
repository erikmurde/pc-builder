using Domain.Base;

namespace DAL.DTO.PcComponent;

public class PcComponentEditDTO : DomainEntityId
{
    public Guid ComponentId { get; set; }
    public Guid PcBuildId { get; set; }
    public string CategoryName { get; set; } = default!;
    public int Qty { get; set; }
}