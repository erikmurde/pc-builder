using Domain.Base;

namespace DAL.DTO.OrderShippingCost;

public class OrderShippingCostDTO : DomainEntityId
{
    public string OrderNr { get; set; } = default!;
    public decimal TotalCost { get; set; }        
}