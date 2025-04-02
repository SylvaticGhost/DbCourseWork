using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface ICardOwnerService
{
    public Task<Result<IEnumerable<CardOwner>>> Search(SearchParameters parameters);
}