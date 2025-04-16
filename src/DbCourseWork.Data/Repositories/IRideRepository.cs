using Core.Models;
using Data.Abstracrtions;

namespace Data.Repositories;

public interface IRideRepository : IRepositoryInstance
{
    public Task InsertRange(IEnumerable<Ride> rideList);
}