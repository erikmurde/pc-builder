using Domain.Base;

namespace BLL.DTO.Payment;

public class PaymentCreateDTO : DomainEntityId
{
    public string PaymentNr { get; set; } = default!;
    public decimal AmountPaid { get; set; }
    public string? Comment { get; set; }
}