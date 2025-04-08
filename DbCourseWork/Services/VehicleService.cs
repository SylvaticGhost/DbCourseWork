using Ardalis.Result;
using DbCourseWork.Models;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;

namespace DbCourseWork.Services;

public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles() =>
        ResultExtensions.InErrorHandler(vehicleRepository.GetAllVehicles);
}