using DAL.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class EFBaseUOW<TDbContext> : IBaseUOW
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext;
    
    protected EFBaseUOW(TDbContext dataContext)
    {
        UowDbContext = dataContext;
    }
    
    public virtual async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}