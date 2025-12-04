using Domain.Base;

namespace BLL.DTO.ShippingMethod;

public class ShippingMethodDTO : DomainEntityId
{
    public string MethodName { get; set; } = default!;
    public string ShippingTime { get; set; } = default!;
}