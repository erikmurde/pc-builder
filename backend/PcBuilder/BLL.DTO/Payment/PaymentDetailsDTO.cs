using Domain.Base;

namespace BLL.DTO.Payment;

public class PaymentDetailsDTO : DomainEntityId
{
    public string CustomerName { get; set; } = default!;
    public string UserEmail { get; set; } = default!;
    public string OrderNr { get; set; } = default!;
    public string PaymentNr { get; set; } = default!;
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Comment { get; set; }
}