namespace DAL.DTO.PcComponent;

public class PcComponentCreateDTO
{
    public Guid ComponentId { get; set; }
    public Guid PcBuildId { get; set; }
    public int Qty { get; set; }
}