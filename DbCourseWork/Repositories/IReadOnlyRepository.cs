using DbCourseWork.Models;
using DbCourseWork.Models.Primitives;

namespace DbCourseWork.Repositories;

public interface IReadOnlyRepository<TEntity> : IRepositoryInstance where TEntity : IReadOnlyDbEntity
{
    public Task<IEnumerable<TEntity>> Get(SearchParameters parameters);
}