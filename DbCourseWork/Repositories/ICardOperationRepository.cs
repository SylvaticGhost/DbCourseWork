using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface ICardOperationRepository : IRepository
{
    public Task InsertRange(IEnumerable<CardOperation> operations);
    
    public Task<IEnumerable<CardOperation>> GetForCard(long card);

    public Task<IEnumerable<RideCardOperation>> GetRidesForCard(long card);
}