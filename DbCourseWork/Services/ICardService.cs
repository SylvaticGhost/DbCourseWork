using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface ICardService : ISearchableService<TravelCard>
{
    public Task<Result<IEnumerable<TravelCard>>> Search(SearchParameters param);

    public Task<Result<IEnumerable<TravelCard>>> GetCardForUser(int userId);
    
    public Task<Result<TravelCard?>> Find(long code);
}