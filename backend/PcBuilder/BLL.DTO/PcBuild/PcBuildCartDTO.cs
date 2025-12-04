using BLL.DTO.PcComponent;
using Domain.Base;

namespace BLL.DTO.PcBuild;

public class PcBuildCartDTO : DomainEntityId
{
    public int DiscountPercentage { get; set; }
    public string PcName { get; set; } = default!;
    public string? ImageSrc { get; set; }
    public bool IsCustom { get; set; }
    public int Stock { get; set; }
    
    public ICollection<PcComponentCartDTO> PcComponents { get; set; } = default!;
}