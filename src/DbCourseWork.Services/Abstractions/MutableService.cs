using Ardalis.Result;
using Core.Interfaces;
using Data.Abstracrtions;
using ResultExtensions = Utils.ResultExtensions;

namespace Services.Abstractions;

public class MutableService<TEntity>(IRepository<TEntity> repository)
    : SearchableService<TEntity>(repository), IMutableService<TEntity>
    where TEntity : class, IDbEntity, IFormTableEntity
{
    public Task<Result> Delete(TEntity entity) => ResultExtensions.InErrorHandler(repository.Delete(entity));
}