using Domain.Base;

namespace DAL.DTO.CartPc;

public class CartPcDTO : DomainEntityId
{
    public Guid PcBuildId { get; init; }
    public int Qty { get; set; }
}