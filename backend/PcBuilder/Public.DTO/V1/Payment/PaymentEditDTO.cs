namespace Public.DTO.V1.Payment;

public class PaymentEditDTO
{
    public Guid Id { get; set; }
    public string Comment { get; set; } = default!;
}