using Domain.Base;

namespace BLL.DTO.CartPc;

public class CartPcEditDTO : DomainEntityId
{
    public Guid PcBuildId { get; set; }
    public int Qty { get; set; }
}