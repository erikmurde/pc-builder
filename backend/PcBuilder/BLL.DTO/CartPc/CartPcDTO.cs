using BLL.DTO.PcBuild;
using Domain.Base;

namespace BLL.DTO.CartPc;

public class CartPcDTO : DomainEntityId
{
    public PcBuildCartDTO PcBuild { get; set; } = default!;
    public int Qty { get; set; }
}