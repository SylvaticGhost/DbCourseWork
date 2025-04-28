using Core.Interfaces;

namespace Data.Abstracrtions;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : IDbEntity
{
    public Task Insert(TEntity entity);

    public Task Delete(TEntity entity);

    public Task Update(TEntity oldEntity, TEntity newEntity);
}