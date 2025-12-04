namespace Public.DTO.V1.OrderShippingCost;

public class OrderShippingCostCreateDTO
{
    public Guid ShippingCostId { get; set; }
    public decimal TotalCost { get; set; }
}