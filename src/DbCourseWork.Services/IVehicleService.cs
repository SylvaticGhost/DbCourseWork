using Ardalis.Result;
using Core.Models;
using Services.Abstractions;

namespace Services;

public interface IVehicleService : IMutableService<Vehicle, Vehicle>
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllVehicles();

    public Task<Result<Vehicle>> Create(Vehicle vehicle);
}