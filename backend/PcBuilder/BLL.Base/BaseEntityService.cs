using BLL.Contracts.Base;
using Contracts.Base;
using DAL.Contracts.Base;

namespace BLL.Base;

public class BaseEntityService<TBllEntity, TDalEntity, TRepository> : 
    BaseEntityService<TBllEntity, TDalEntity, TRepository, Guid>, IEntityService<TBllEntity>
    where TBllEntity : class
    where TDalEntity : class
    where TRepository : IBaseRepository<TDalEntity>
{
    protected BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper) : 
        base(repository, mapper)
    {
    }
}

public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TBllEntity : class
    where TDalEntity : class
    where TRepository : IBaseRepository<TDalEntity, TKey>
    where TKey : struct, IEquatable<TKey>
{
    protected readonly TRepository Repository;
    protected readonly IMapper<TBllEntity, TDalEntity> Mapper;

    protected BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    
    public virtual async Task<IEnumerable<TBllEntity>> AllAsync()
    {
        return (await Repository.AllAsync()).Select(e => Mapper.Map(e));
    }

    public virtual async Task<TBllEntity?> FindAsync(TKey id)
    {
        return Mapper.Map(await Repository.FindAsync(id));
    }

    public virtual TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)));
    }

    public virtual TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)));
    }

    public virtual TBllEntity Remove(TBllEntity entity)
    {
        return Mapper.Map(Repository.Remove(Mapper.Map(entity)));
    }

    public virtual async Task<TBllEntity?> RemoveAsync(TKey id)
    {
        return Mapper.Map(await Repository.RemoveAsync(id));
    }
}