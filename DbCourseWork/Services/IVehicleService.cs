using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface IVehicleService
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles();
}