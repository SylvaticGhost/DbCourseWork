using System.Text;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public class VehicleRepository(DataContext dataContext) : IVehicleRepository
{
    public Task<IEnumerable<Vehicle>> GetAllVehicles()
    {
        const string sql = "SELECT * FROM Vehicles";
        return dataContext.LoadData<Vehicle>(sql);
    }

    public async Task InsertVehicle(params IEnumerable<Vehicle> vehicles)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO Vehicles (number, type)  VALUES");
        
        foreach (var vehicle in vehicles)
            sb.AppendLine($"('{vehicle.Number}', '{(short)vehicle.Type}'),");

        var sql = sb.EndSql().ToString();
        
        await dataContext.ExecuteSql(sql);
    }
}