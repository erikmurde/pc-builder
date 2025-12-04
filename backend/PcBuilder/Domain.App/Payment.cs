using System.ComponentModel.DataAnnotations;
using DAL.EF.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Payment : DomainEntityId
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    [MaxLength(12)]
    public string PaymentNr { get; set; } = default!;
    
    [DecimalRange(8, 2, ErrorMessage = "Amount must be a decimal between 0 and 99,999.99")]
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    [MaxLength(2048)]
    public string? Comment { get; set; }
}