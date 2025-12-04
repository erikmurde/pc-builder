namespace DAL.Contracts.Base;

public interface IBaseRepository<TDalEntity> : IBaseRepository<TDalEntity, Guid>
    where TDalEntity : class
{
}

public interface IBaseRepository<TDalEntity, in TKey>
    where TDalEntity : class
    where TKey : struct, IEquatable<TKey>
{
    Task<IEnumerable<TDalEntity>> AllAsync();
    
    Task<TDalEntity?> FindAsync(TKey id);

    TDalEntity Add(TDalEntity entity);
    
    TDalEntity Update(TDalEntity entity);

    TDalEntity Remove(TDalEntity entity);
    
    Task<TDalEntity?> RemoveAsync(TKey id);
}