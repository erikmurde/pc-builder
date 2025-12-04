using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class PcBuild : DomainEntityId
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid DiscountId { get; set; }
    public Discount? Discount { get; set; }

    [MaxLength(64)]
    public string PcName { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    
    [MaxLength(256)]
    public string? ImageSrc { get; set; }

    public ICollection<PcComponent>? PcComponents { get; set; }
    public ICollection<OrderPc>? OrderPcs { get; set; }
    public ICollection<CartPc>? CartPcs { get; set; }
    public ICollection<UserReview>? UserReviews { get; set; }
}