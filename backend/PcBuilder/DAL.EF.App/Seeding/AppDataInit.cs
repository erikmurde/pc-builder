using DAL.EF.Base;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Seeding;

public static class AppDataInit
{
    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }
    
    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }
    
    public static void SeedIdentity(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        (Guid id, string name) roleDataAdmin = (AppDataIds.AdminRoleId, UserRole.Admin);  
        (Guid id, string name) roleDataStandard = (AppDataIds.StandardRoleId, UserRole.Standard); 
        (Guid id, string email, string password) userDataAdmin = (AppDataIds.AdminId, "admin@app.com", "Admin.1");
        (Guid id, string email, string password) userDataTestUser = (AppDataIds.UserId, "testuser@app.com", "Test.1");
        
        var roleAdmin = SeedIdentityRole(roleManager, roleDataAdmin);
        var roleStandard = SeedIdentityRole(roleManager, roleDataStandard);
        var userAdmin = SeedIdentityUser(userManager, userDataAdmin);
        var userTestUser = SeedIdentityUser(userManager, userDataTestUser);

        AddUserToRole(userManager, userAdmin, roleAdmin);
        AddUserToRole(userManager, userTestUser, roleStandard);
    }

    private static ApplicationRole SeedIdentityRole(
        RoleManager<ApplicationRole> roleManager, (Guid id, string name) roleData)
    {
        var role = roleManager.FindByNameAsync(roleData.name).Result;
        if (role == null)
        {
            role = new ApplicationRole
            {
                Id = roleData.id,
                Name = roleData.name
            };

            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed roles, {result}");
            }
        }
        return role;
    }
    
    private static ApplicationUser SeedIdentityUser(
        UserManager<ApplicationUser> userManager, (Guid id, string email, string password) userData)
    {
        var user = userManager.FindByEmailAsync(userData.email).Result;
        if (user == null)
        {
            user = new ApplicationUser
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                EmailConfirmed = true
            };
            
            var result = userManager.CreateAsync(user, userData.password).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed users, {result}");
            }
        }
        return user;
    }

    private static void AddUserToRole(
        UserManager<ApplicationUser> userManager, ApplicationUser user, ApplicationRole role)
    {
        var result = userManager.AddToRoleAsync(user, role.Name!).Result;
        if (!result.Succeeded)
        {
            throw new ApplicationException($"Cannot add user to role, {result}");
        }   
    }
    
    public static void SeedAppData(ApplicationDbContext context)
    {
        var seeder = new AppDataSeeding(context);

        seeder.SeedAppDataCategory();
        seeder.SeedAppDataDiscount();
        seeder.SeedAppDataManufacturer();
        seeder.SeedAppDataStatus();
        seeder.SeedAppDataPackageSize();
        seeder.SeedAppDataShippingMethod();
        seeder.SeedAppDataPcBuild();
        seeder.SeedAppDataComponents();
        seeder.SeedAppDataPcComponent();
        seeder.SeedAppDataAttribute();
        seeder.SeedAppDataComponentAttribute();
        seeder.SeedAppDataShippingCost();
        seeder.SeedAppDataUserReview();

        context.SaveChanges();
    }
}