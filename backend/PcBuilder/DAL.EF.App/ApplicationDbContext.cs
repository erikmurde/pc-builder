using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Attribute = Domain.App.Attribute;

namespace DAL.EF.App;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public DbSet<Attribute> Attributes { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Component> Components { get; set; } = default!;
    public DbSet<ComponentAttribute> ComponentAttributes { get; set; } = default!;
    public DbSet<PcComponent> PcComponents { get; set; } = default!;
    public DbSet<Discount> Discounts { get; set; } = default!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderShippingCost> OrderShippingCosts { get; set; } = default!;
    public DbSet<PackageSize> PackageSizes { get; set; } = default!;
    public DbSet<Payment> Payments { get; set; } = default!;
    public DbSet<PcBuild> PcBuilds { get; set; } = default!;
    public DbSet<CartPc> CartPcs { get; set; } = default!;
    public DbSet<OrderPc> OrderPcs { get; set; } = default!;
    public DbSet<ShippingCost> ShippingCosts { get; set; } = default!;
    public DbSet<ShippingMethod> ShippingMethods { get; set; } = default!;
    public DbSet<Status> Statuses { get; set; } = default!;
    public DbSet<UserReview> UserReviews { get; set; } = default!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // disable cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}