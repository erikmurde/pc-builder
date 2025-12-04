using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.EF.App;

public class IdentityDbContext<TUser, TRole>
    : IdentityDbContext<TUser, TRole, Guid>
    where TUser : ApplicationUser
    where TRole : ApplicationRole
{
}