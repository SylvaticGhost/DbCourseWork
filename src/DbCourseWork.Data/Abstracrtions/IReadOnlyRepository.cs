using Core.Interfaces;
using Core.Models.Systems;

namespace Data.Abstracrtions;

public interface IReadOnlyRepository<TEntity> : IRepositoryInstance where TEntity : IReadOnlyDbEntity
{
    public Task<IEnumerable<TEntity>> Get(SearchParameters parameters);
}