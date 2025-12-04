namespace Public.DTO.V1.Payment;

public class PaymentDTO
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = default!;
    public string OrderNr { get; set; } = default!;
    public string PaymentNr { get; set; } = default!;
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
}