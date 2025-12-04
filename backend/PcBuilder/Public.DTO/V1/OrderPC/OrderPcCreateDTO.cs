namespace Public.DTO.V1.OrderPC;

public class OrderPcCreateDTO
{
    public Guid PcBuildId { get; set; }
    public Guid PackageSizeId { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Qty { get; set; }
}