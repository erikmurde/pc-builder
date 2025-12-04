using Domain.Base;

namespace DAL.DTO.ShippingMethod;

public class ShippingMethodDTO : DomainEntityId
{
    public string MethodName { get; set; } = default!;
    
    public string ShippingTime { get; set; } = default!;
}