using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface ICardRepository : IRepository
{
    public Task<IEnumerable<TravelCard>> Get(CardSearchParam parameters);

    public Task<IEnumerable<TravelCard>> GetCardForUser(int userId);

    public Task<TravelCard?> Find(long code);
}