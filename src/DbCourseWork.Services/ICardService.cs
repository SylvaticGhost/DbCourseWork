using Ardalis.Result;
using Core.Models;

namespace Services;

public interface ICardService : ISearchableService<TravelCard>
{
    public Task<Result<IEnumerable<TravelCard>>> GetCardForUser(int userId);
    
    public Task<Result<TravelCard?>> Find(long code);
}