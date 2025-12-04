using DAL.Contracts.Base;
using DAL.DTO.Status;

namespace DAL.Contracts.App;

public interface IStatusRepository : IBaseRepository<StatusDTO>
{
    public Task<string?> GetNameById(Guid id);
}