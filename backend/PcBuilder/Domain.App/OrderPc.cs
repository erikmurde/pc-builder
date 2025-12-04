using Domain.Base;

namespace Domain.App;

public class OrderPc : DomainEntityId
{
    public Guid PcBuildId { get; set; }
    public PcBuild? PcBuild { get; set; }

    public Guid PackageSizeId { get; set; }
    public PackageSize? PackageSize { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public decimal PricePerUnit { get; set; }
    public int Qty { get; set; }
}