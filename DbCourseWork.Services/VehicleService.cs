using Ardalis.Result;
using Core.Models;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;

namespace DbCourseWork.Services;

public class VehicleService(IVehicleRepository vehicleRepository) : SearchableService<Vehicle>(vehicleRepository),IVehicleService
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles() =>
        ResultExtensions.InErrorHandler(vehicleRepository.GetAllVehicles);
}