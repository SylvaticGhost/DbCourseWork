using Core.Models;
using Core.Models.Systems;
using Data.Abstracrtions;
using Data.Context;

namespace Data.Repositories;

public class VehicleRepository(DataContext dataContext) : Repository<Vehicle>(dataContext),IVehicleRepository
{
    private readonly DataContext _dataContext = dataContext;

    public Task<IEnumerable<Vehicle>> GetAllVehicles()
    {
        const string sql = "SELECT * FROM Vehicles";
        return _dataContext.LoadData<Vehicle>(sql);
    }

    protected override SortingField DefaultSortingField => new("number");
    protected override string CollectionName => "vehicles";
    protected override string[] Columns => Vehicle.Columns;
}