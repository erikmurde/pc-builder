namespace Public.DTO.V1.Payment;

public class PaymentCreateDTO
{
    public string PaymentNr { get; set; } = default!;
    public decimal AmountPaid { get; set; }
    public string? Comment { get; set; }
}