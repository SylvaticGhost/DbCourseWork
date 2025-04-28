using Ardalis.Result;
using Core.Models;
using Core.Validations;
using Data.Repositories;
using Services.Abstractions;
using Utils;
using ResultExtensions = Utils.ResultExtensions;

namespace Services;

public class VehicleService(IVehicleRepository vehicleRepository)
    : MutableService<Vehicle>(vehicleRepository), IVehicleService
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles() =>
        ResultExtensions.InErrorHandler(vehicleRepository.GetAllVehicles);

    public Task<Result<Vehicle>> Create(Vehicle vehicle)
    {
        var validationResult = Validator.Use<VehicleValidator, Vehicle>(vehicle);
        if (!validationResult.IsSuccess)
            return Task.FromResult((Result<Vehicle>)validationResult);
        
        return ResultExtensions.InErrorHandler(async () =>
        {
            await vehicleRepository.Insert(vehicle);
            return vehicle;
        });
    }
}