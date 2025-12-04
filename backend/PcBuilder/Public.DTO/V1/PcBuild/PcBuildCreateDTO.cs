using DTO.Base;

namespace Public.DTO.V1.PcBuild;

public class PcBuildCreateDTO : PcBuildBaseDTO
{
    public Guid CategoryId { get; set; }
    public Guid DiscountId { get; set; }
}