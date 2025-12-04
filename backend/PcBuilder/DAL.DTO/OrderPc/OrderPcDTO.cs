namespace DAL.DTO.OrderPc;

public class OrderPcDTO
{
    public Guid PcBuildId { get; set; }
    public string PackageSize { get; set; } = default!;
    public decimal PricePerUnit { get; set; }
    public int Qty { get; set; }
}