using Contracts.Base;
using DAL.Contracts.Base;
using Domain.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class EFBaseRepository<TDalEntity, TDomainEntity, TDbContext> : 
    EFBaseRepository<TDalEntity, TDomainEntity, Guid, TDbContext>, IBaseRepository<TDalEntity>

    where TDalEntity : class
    where TDomainEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    protected EFBaseRepository(TDbContext dataContext, IMapper<TDalEntity, TDomainEntity> mapper) : 
        base(dataContext, mapper)
    {
    }
}

public class EFBaseRepository<TDalEntity, TDomainEntity, TKey, TDbContext> : 
    IBaseRepository<TDalEntity, TKey> 

    where TDalEntity : class 
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
    where TDbContext : DbContext
{
    protected readonly DbSet<TDomainEntity> RepositoryDbSet;
    private readonly IMapper<TDalEntity, TDomainEntity> _mapper;

    protected EFBaseRepository(TDbContext dataContext, IMapper<TDalEntity, TDomainEntity> mapper)
    {
        RepositoryDbSet = dataContext.Set<TDomainEntity>();
        _mapper = mapper;
    }

    public virtual async Task<IEnumerable<TDalEntity>> AllAsync()
    {
        return (await RepositoryDbSet
            .AsNoTracking()
            .ToListAsync())
            .Select(e => _mapper.Map(e));
    }

    public virtual async Task<TDalEntity?> FindAsync(TKey id)
    {
        return _mapper.Map(await RepositoryDbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(id)));
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        return _mapper.Map(RepositoryDbSet.Add(_mapper.Map(entity)).Entity);
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        return _mapper.Map(RepositoryDbSet.Update(_mapper.Map(entity)).Entity);
    }

    public virtual TDalEntity Remove(TDalEntity entity)
    {
        return _mapper.Map(RepositoryDbSet.Remove(_mapper.Map(entity)).Entity);
    }

    public virtual async Task<TDalEntity?> RemoveAsync(TKey id)
    {
        var entity = await RepositoryDbSet.FindAsync(id);
        return entity != null ? _mapper.Map(RepositoryDbSet.Remove(entity).Entity) : null;
    }
}