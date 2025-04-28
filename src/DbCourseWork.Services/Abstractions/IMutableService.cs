using Ardalis.Result;
using Core.Interfaces;

namespace Services.Abstractions;

public interface IMutableService<TEntity> : ISearchableService<TEntity>
    where TEntity : class, IDbEntity, IFormTableEntity
{
    Task<Result> Delete(TEntity entity);
}
