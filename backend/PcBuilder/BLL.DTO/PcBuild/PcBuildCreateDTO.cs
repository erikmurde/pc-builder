using DTO.Base;

namespace BLL.DTO.PcBuild;

public class PcBuildCreateDTO : PcBuildBaseDTO
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid DiscountId { get; set; }
}