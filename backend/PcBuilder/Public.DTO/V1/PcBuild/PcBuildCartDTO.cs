using Public.DTO.V1.PcComponent;

namespace Public.DTO.V1.PcBuild;

public class PcBuildCartDTO
{
    public Guid Id { get; set; }
    public int DiscountPercentage { get; set; }
    public string PcName { get; set; } = default!;
    public string? ImageSrc { get; set; }
    public bool IsCustom { get; set; }
    public int Stock { get; set; }
    
    public ICollection<PcComponentCartDTO> PcComponents { get; set; } = default!;
}