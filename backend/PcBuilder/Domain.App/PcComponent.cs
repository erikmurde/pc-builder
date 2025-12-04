using Domain.Base;

namespace Domain.App;

public class PcComponent : DomainEntityId
{
    public Guid ComponentId { get; set; }
    public Component? Component { get; set; }

    public Guid PcBuildId { get; set; }
    public PcBuild? PcBuild { get; set; }

    public int Qty { get; set; }
}