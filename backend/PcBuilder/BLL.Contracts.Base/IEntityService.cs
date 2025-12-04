using DAL.Contracts.Base;

namespace BLL.Contracts.Base;

public interface IEntityService<TEntity> : IBaseRepository<TEntity>, IEntityService<TEntity, Guid>
    where TEntity : class
{
}

public interface IEntityService<TEntity, in TKey> : IBaseRepository<TEntity, TKey>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class
{
}