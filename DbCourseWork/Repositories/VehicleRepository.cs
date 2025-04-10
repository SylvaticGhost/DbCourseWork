using System.Text;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public class VehicleRepository(DataContext dataContext) : Repository<Vehicle>(dataContext),IVehicleRepository
{
    private readonly DataContext _dataContext = dataContext;

    public Task<IEnumerable<Vehicle>> GetAllVehicles()
    {
        const string sql = "SELECT * FROM Vehicles";
        return _dataContext.LoadData<Vehicle>(sql);
    }

    public async Task InsertVehicle(params IEnumerable<Vehicle> vehicles)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INSERT INTO Vehicles (number, type)  VALUES");
        
        foreach (var vehicle in vehicles)
            sb.AppendLine($"('{vehicle.Number}', '{(short)vehicle.Type}'),");

        var sql = sb.EndSql().ToString();
        
        await _dataContext.ExecuteSql(sql);
    }

    protected override SortingField DefaultSortingField => new("number");
    protected override string CollectionName => "vehicles";
    protected override string[] Columns => Vehicle.Columns;
}