using Ardalis.Result;
using Core.Models;

namespace Services;

public interface IVehicleService : ISearchableService<Vehicle>
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles();
}