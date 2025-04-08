using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface ICardService
{
    public Task<Result<IEnumerable<TravelCard>>> Search(CardSearchParam param);

    public Task<Result<IEnumerable<TravelCard>>> GetCardForUser(int userId);
    
    public Task<Result<TravelCard?>> Find(long code);
}