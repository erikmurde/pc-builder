using Domain.App;
using Attribute = Domain.App.Attribute;

namespace DAL.EF.App.Seeding;

public class AppDataSeeding
{
    private readonly ApplicationDbContext _context;
    
    public AppDataSeeding(ApplicationDbContext context)
    {
        _context = context;
    }
    
    internal void SeedAppDataCategory()
    {
        foreach (var testCategory in AppDataIds.CategoryIds.Select(
                     category => new Category 
                     {
                         Id = category.Value,
                         CategoryName = category.Key,
                     }))
        {
            _context.Categories.Add(testCategory);
        }
    }
    
    internal void SeedAppDataManufacturer()
    {
        foreach (var testManufacturer in AppDataIds.ManufacturerIds.Select(
                     manufacturer => new Manufacturer
                     {
                         Id = manufacturer.Value,
                         ManufacturerName = manufacturer.Key
                     }))
        {
            _context.Manufacturers.Add(testManufacturer);
        }
    }

    internal void SeedAppDataDiscount()
    {
        AddDiscount(AppDataIds.Discount0, "No Discount", 0);
        AddDiscount(AppDataIds.Discount10, "10% Discount", 10);
        AddDiscount(AppDataIds.Discount20, "20% Discount", 20);
        AddDiscount(AppDataIds.Discount30, "30% Discount", 30);
        AddDiscount(AppDataIds.Discount40, "40% Discount", 40);
        AddDiscount(AppDataIds.Discount50, "50% Discount", 50);
    }

    private void AddDiscount(Guid id, string name, int percent)
    {
        _context.Discounts.Add(new Discount
        {
            Id = id,
            DiscountName = name,
            DiscountPercentage = percent
        });
    }

    internal void SeedAppDataStatus()
    {
        var statuses = new List<string> 
            {"Placed", "Confirmed", "In assembly", "In shipping", "Cancelled", "Completed"};

        foreach (var testStatus in statuses.Select(
                     status => new Status { StatusName = status }))
        {
            testStatus.Id = testStatus.StatusName switch
            {
                "Cancelled" => AppDataIds.CancelledStatusId,
                "Completed" => AppDataIds.CompletedStatusId,
                "Placed" => AppDataIds.PlacedStatusId,
                _ => testStatus.Id
            };

            _context.Add(testStatus);
        }
    }

    internal void SeedAppDataPackageSize()
    {
        _context.Add(new PackageSize { Id = AppDataIds.SmallSizeId, SizeName = "Small" });
        _context.Add(new PackageSize { Id = AppDataIds.MediumSizeId, SizeName = "Medium" });
        _context.Add(new PackageSize { Id = AppDataIds.LargeSizeId, SizeName = "Large" });
    }

    internal void SeedAppDataShippingMethod()
    {
        _context.ShippingMethods.Add(new ShippingMethod
        {
            Id = AppDataIds.StandardShippingId,
            MethodName = "Standard shipping",
            ShippingTime = "10 days"
        });
        _context.ShippingMethods.Add(new ShippingMethod
        {
            Id = AppDataIds.ExpressShippingId,
            MethodName = "Express shipping",
            ShippingTime = "5 days"
        });
    }

    internal void SeedAppDataPcBuild()
    {
        var seeder = new AppDataSeedingPcBuilds(_context);
        seeder.SeedAppDataPcBuilds();
    }

    internal void SeedAppDataComponents()
    {
        var seeder = new AppDataSeedingComponent(_context);
        seeder.SeedAppDataComponents();
    }

    internal void SeedAppDataPcComponent()
    {
        var seeder = new AppDataSeedingPcComponents(_context);
        seeder.SeedAppDataPcComponents();
    }

    internal void SeedAppDataAttribute()
    {
        foreach (var testAttribute in AppDataIds.AttributeIds.Select(
                     attribute => new Attribute
                     {
                         Id = attribute.Value,
                         AttributeName = attribute.Key
                     }))
        {
            _context.Attributes.Add(testAttribute);
        }
    }

    internal void SeedAppDataComponentAttribute()
    {
        var seeder = new AppDataSeedingComponentAttribute(_context);
        seeder.SeedAppDataComponentAttributes();
    }

    internal void SeedAppDataShippingCost()
    {
        AddShippingCost(AppDataIds.SmallSizeId, AppDataIds.StandardShippingId, 50m);
        AddShippingCost(AppDataIds.MediumSizeId, AppDataIds.StandardShippingId, 70m, 
            AppDataIds.ShippingCostId);
        AddShippingCost(AppDataIds.LargeSizeId, AppDataIds.StandardShippingId, 100m);
        AddShippingCost(AppDataIds.SmallSizeId, AppDataIds.ExpressShippingId, 70m);
        AddShippingCost(AppDataIds.MediumSizeId, AppDataIds.ExpressShippingId, 100m);
        AddShippingCost(AppDataIds.LargeSizeId, AppDataIds.ExpressShippingId, 130m);
    }

    private void AddShippingCost(Guid packageSizeId, Guid shippingMethodId, decimal costPerUnit, Guid? id = null)
    {
        _context.ShippingCosts.Add(new ShippingCost
        {
            Id = id ?? Guid.NewGuid(),
            PackageSizeId = packageSizeId,
            ShippingMethodId = shippingMethodId,
            CostPerUnit = costPerUnit
        });
    }

    internal void SeedAppDataUserReview()
    {
        _context.UserReviews.Add(new UserReview
        {
            ApplicationUserId = AppDataIds.AdminId,
            PcBuildId = AppDataIds.PcPrebuilt1,
            Rating = 4,
            ReviewContent = "This is a test review."
        });
        _context.UserReviews.Add(new UserReview
        {
            ApplicationUserId = AppDataIds.AdminId,
            PcBuildId = AppDataIds.PcPrebuilt1,
            Rating = 3,
            ReviewContent = "Another test review."
        });
    }
}