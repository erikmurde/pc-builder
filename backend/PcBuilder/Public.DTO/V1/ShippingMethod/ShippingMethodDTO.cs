namespace Public.DTO.V1.ShippingMethod;

public class ShippingMethodDTO
{
    public Guid Id { get; set; }
    
    public string MethodName { get; set; } = default!;
    
    public string ShippingTime { get; set; } = default!;
}