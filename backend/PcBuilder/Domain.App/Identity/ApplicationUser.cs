using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class ApplicationUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<CartPc>? CartPcs { get; set; }
    public ICollection<UserReview>? UserReviews { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Payment>? Payments { get; set; }
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}