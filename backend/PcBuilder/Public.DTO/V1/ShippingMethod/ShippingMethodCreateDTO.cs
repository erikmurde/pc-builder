namespace Public.DTO.V1.ShippingMethod;

public class ShippingMethodCreateDTO
{
    public string MethodName { get; set; } = default!;
    
    public string ShippingTime { get; set; } = default!;
}