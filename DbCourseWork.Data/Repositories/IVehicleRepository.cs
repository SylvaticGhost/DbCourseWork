using Core.Models;

namespace DbCourseWork.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    public Task<IEnumerable<Vehicle>> GetAllVehicles();
}