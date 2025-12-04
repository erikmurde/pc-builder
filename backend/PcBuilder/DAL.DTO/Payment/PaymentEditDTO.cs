using Domain.Base;

namespace DAL.DTO.Payment;

public class PaymentEditDTO : DomainEntityId
{
    public string Comment { get; set; } = default!;
}