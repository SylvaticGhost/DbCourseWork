using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface IRideRepository : IRepository
{
    public Task InsertRange(IEnumerable<Ride> rideList);
}