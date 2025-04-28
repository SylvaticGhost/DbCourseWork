using Ardalis.Result;
using Core.Models;
using Core.Models.DTOs;
using Core.Validations;
using Data.Repositories;
using Services.Abstractions;
using Utils;
using ResultExtensions = Utils.Extensions.ResultExtensions;

namespace Services;

public class VehicleService(IVehicleRepository vehicleRepository)
    : MutableService<Vehicle, Vehicle, VehicleCreateValidator>(vehicleRepository), IVehicleService
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles() =>
        ResultExtensions.InErrorHandler(vehicleRepository.GetAllVehicles);

    protected override Vehicle FabricMethod(Vehicle createDto) => createDto;

    protected override Task<Result<Vehicle>> CreateChecks(Vehicle entity) =>
        Task.FromResult(Result<Vehicle>.Success(entity));
}