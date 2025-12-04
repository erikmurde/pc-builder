using Domain.Base;

namespace DAL.DTO.OrderPc;

public class OrderPcCreateDTO : DomainEntityId
{
    public Guid PcBuildId { get; set; }
    public Guid PackageSizeId { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Qty { get; set; }
}