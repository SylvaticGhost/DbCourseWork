using Ardalis.Result;
using DbCourseWork.Models;
using DbCourseWork.Models.Primitives;

namespace DbCourseWork.Services;

public interface ISearchableService<TEntity> where TEntity : IFormTableEntity
{
    public Task<Result<IEnumerable<TEntity>>> Search(SearchParameters param);
}