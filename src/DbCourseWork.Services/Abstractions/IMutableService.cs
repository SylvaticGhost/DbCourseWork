using Ardalis.Result;
using Core.Interfaces;

namespace Services.Abstractions;

public interface IMutableService<TEntity, in TCreateDto> : ISearchableService<TEntity>
    where TEntity : class, IDbEntity, IFormTableEntity
{
    public Task<Result> Delete(TEntity entity);

    public Task<Result<TEntity>> Update(TEntity entity, TCreateDto dto);
}
