using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface IVehicleService : ISearchableService<Vehicle>
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles();
}