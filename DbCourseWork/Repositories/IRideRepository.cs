using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface IRideRepository : IRepositoryInstance
{
    public Task InsertRange(IEnumerable<Ride> rideList);
}