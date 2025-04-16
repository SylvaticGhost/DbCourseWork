using Ardalis.Result;
using Core.Models;
using Data.Repositories;
using ResultExtensions = Utils.ResultExtensions;

namespace Services;

public class VehicleService(IVehicleRepository vehicleRepository) : SearchableService<Vehicle>(vehicleRepository),IVehicleService
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles() =>
        ResultExtensions.InErrorHandler(vehicleRepository.GetAllVehicles);
}