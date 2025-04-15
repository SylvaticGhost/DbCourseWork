using Core.Models;

namespace DbCourseWork.Repositories;

public interface ICardRepository : IRepository<TravelCard>
{
    public Task<IEnumerable<TravelCard>> GetCardForUser(int userId);

    public Task<TravelCard?> Find(long code);
}