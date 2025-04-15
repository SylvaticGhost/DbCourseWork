using Ardalis.Result;
using Core.Interfaces;
using Core.Models.Systems;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;

namespace DbCourseWork.Services;

public class SearchableService<TEntity>(IReadOnlyRepository<TEntity> repository)
    : ISearchableService<TEntity> where TEntity : IReadOnlyDbEntity, IFormTableEntity
{
    public Task<Result<IEnumerable<TEntity>>> Search(SearchParameters param) =>
        ResultExtensions.InErrorHandler(repository.Get(param));
}