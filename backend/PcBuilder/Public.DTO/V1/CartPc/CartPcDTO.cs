using Public.DTO.V1.PcBuild;

namespace Public.DTO.V1.CartPc;

public class CartPcDTO
{   
    public Guid Id { get; set; }
    public PcBuildCartDTO PcBuild { get; set; } = default!;
    public int Qty { get; set; }
}