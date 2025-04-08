using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface ICardOperationService
{
    public Task<Result<IEnumerable<RideCardOperation>>> GetRides(long card);
}