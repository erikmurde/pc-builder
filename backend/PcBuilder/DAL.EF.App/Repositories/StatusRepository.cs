using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO.Status;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class StatusRepository :
    EFBaseRepository<StatusDTO, Status, ApplicationDbContext>, IStatusRepository
{
    public StatusRepository(ApplicationDbContext dataContext, IMapper<StatusDTO, Status> mapper) : 
        base(dataContext, mapper)
    {
    }
    
    public async Task<string?> GetNameById(Guid id)
    {
        return await RepositoryDbSet
            .Where(s => s.Id == id)
            .Select(s => s.StatusName)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}