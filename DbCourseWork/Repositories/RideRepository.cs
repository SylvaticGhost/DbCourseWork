using System.Text;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public class RideRepository(DataContext dataContext) : Repository<Ride>(dataContext),IRideRepository
{
    private readonly DataContext _dataContext = dataContext;

    // public Task InsertRange(IEnumerable<Ride> rideList)
    // {
    //     var sb = new StringBuilder().AppendLine("INSERT INTO rides (id, vehicle, route) VALUES ");
    //     foreach (var ride in rideList)
    //         sb.AppendLine($"('{ride.Id}', {ride.Vehicle}, '{ride.Route}'),");
    //
    //     var sql = sb.EndSql().ToString();
    //     return _dataContext.ExecuteSql(sql);
    // }

    protected override string CollectionName => "rides";
    protected override string[] Columns => Ride.Columns;
}