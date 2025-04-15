using Ardalis.Result;
using Core.Models;
using Core.Models.Systems;

namespace DbCourseWork.Services;

public interface ICardService : ISearchableService<TravelCard>
{
    public Task<Result<IEnumerable<TravelCard>>> GetCardForUser(int userId);
    
    public Task<Result<TravelCard?>> Find(long code);
}