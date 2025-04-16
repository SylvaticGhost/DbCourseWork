using Core.Models;
using Data.Abstracrtions;

namespace Data.Repositories;

public interface ICardOperationRepository : IRepositoryInstance
{
    public Task InsertRange(IEnumerable<CardOperation> operations);
    
    public Task<IEnumerable<CardOperation>> GetForCard(long card);

    public Task<IEnumerable<RideCardOperation>> GetRidesForCard(long card);
}