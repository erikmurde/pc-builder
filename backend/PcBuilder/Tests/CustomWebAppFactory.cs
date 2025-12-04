using DAL.EF.App;
using DAL.EF.App.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests;

public class CustomWebAppFactory<TStartup> : WebApplicationFactory<TStartup> 
    where TStartup : class
{
    public static CustomWebAppFactory<TStartup> CreateInstance() => new CustomWebAppFactory<TStartup>();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        
        builder.ConfigureServices(services =>
        {
            // find DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));

            // if found - remove
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            // add new DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
            
            // create db and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            using var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<CustomWebAppFactory<TStartup>>>();

            db.Database.EnsureCreated();
            
            try
            {
                AppDataInit.SeedAppData(db);
            }
            catch (Exception e)
            {
                logger.LogError("Error seeding data: {Message}", e.Message);
            }
        });
    }
}