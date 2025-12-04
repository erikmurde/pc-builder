using Domain.Base;

namespace BLL.DTO.Discount;

public class DiscountDTO : DomainEntityId
{
    public string DiscountName { get; set; } = default!;
    public int DiscountPercentage { get; set; }
}