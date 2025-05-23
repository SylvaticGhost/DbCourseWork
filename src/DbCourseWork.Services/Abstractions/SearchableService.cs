using Ardalis.Result;
using Core.Interfaces;
using Core.Models.Systems;
using Data.Abstracrtions;
using ResultExtensions = Utils.Extensions.ResultExtensions;

namespace Services.Abstractions;

public class SearchableService<TEntity>(IReadOnlyRepository<TEntity> repository)
    : ISearchableService<TEntity> where TEntity : IReadOnlyDbEntity, IFormTableEntity
{
    public Task<Result<IEnumerable<TEntity>>> Search(SearchParameters param) =>
        ResultExtensions.InErrorHandler(repository.Get(param));
}