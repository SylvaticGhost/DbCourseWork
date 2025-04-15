using Ardalis.Result;
using Core.Models;

namespace DbCourseWork.Services;

public interface ICardOperationService
{
    public Task<Result<IEnumerable<RideCardOperation>>> GetRides(long card);
}