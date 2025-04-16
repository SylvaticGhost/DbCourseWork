using Ardalis.Result;
using Core.Models;

namespace Services;

public interface ICardOperationService
{
    public Task<Result<IEnumerable<RideCardOperation>>> GetRides(long card);
}