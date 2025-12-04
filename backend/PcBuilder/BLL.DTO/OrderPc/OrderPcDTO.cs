using BLL.DTO.PcBuild;

namespace BLL.DTO.OrderPc;

public class OrderPcDTO
{
    public PcBuildDTO PcBuild { get; set; } = default!;
    public string PackageSize { get; set; } = default!;
    public decimal PricePerUnit { get; set; }
    public int Qty { get; set; }
}