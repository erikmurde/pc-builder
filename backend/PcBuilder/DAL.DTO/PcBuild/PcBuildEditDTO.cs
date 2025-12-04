using DTO.Base;

namespace DAL.DTO.PcBuild;

public class PcBuildEditDTO : PcBuildBaseDTO
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid DiscountId { get; set; }
}