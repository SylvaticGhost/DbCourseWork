using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    public Task<IEnumerable<Vehicle>> GetAllVehicles();

    public Task InsertVehicle(params IEnumerable<Vehicle> vehicles);
}