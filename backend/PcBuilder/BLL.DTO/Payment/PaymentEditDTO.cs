using Domain.Base;

namespace BLL.DTO.Payment;

public class PaymentEditDTO : DomainEntityId
{
    public string Comment { get; set; } = default!;   
}