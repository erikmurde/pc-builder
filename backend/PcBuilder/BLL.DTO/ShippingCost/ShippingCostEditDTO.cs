using DTO.Base;

namespace BLL.DTO.ShippingCost;

public class ShippingCostEditDTO : ShippingCostBaseDTO
{
    public Guid Id { get; set; }
    public Guid PackageSizeId { get; set; }
    public Guid ShippingMethodId { get; set; }
}