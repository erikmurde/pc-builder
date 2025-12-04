namespace BLL.DTO.Payment;

public class PaymentSimpleDTO
{
    public string PaymentNr { get; set; } = default!;
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
}