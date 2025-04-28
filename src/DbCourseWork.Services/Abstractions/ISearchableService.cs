using Ardalis.Result;
using Core.Interfaces;
using Core.Models.Systems;

namespace Services.Abstractions;

public interface ISearchableService<TEntity> where TEntity : IFormTableEntity
{
    public Task<Result<IEnumerable<TEntity>>> Search(SearchParameters param);
}