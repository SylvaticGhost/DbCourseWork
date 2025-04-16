using Core.Models;
using Data.Abstracrtions;

namespace Data.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    public Task<IEnumerable<Vehicle>> GetAllVehicles();
}