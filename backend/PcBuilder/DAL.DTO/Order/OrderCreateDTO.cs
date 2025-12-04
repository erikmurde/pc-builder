using Domain.Base;

namespace DAL.DTO.Order;

public class OrderCreateDTO : DomainEntityId
{
    public Guid StatusId { get; set; }
    public Guid DiscountId { get; set; }
    public Guid ShippingMethodId { get; set; }
    public string OrderNr { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public string CustomerPhoneNumber { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShippingPostalCode { get; set; } = default!;
    public string? Comment { get; set; }
}