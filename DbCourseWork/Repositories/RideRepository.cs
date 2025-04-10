using DbCourseWork.Data;
using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public class RideRepository(DataContext dataContext) : Repository<Ride>(dataContext),IRideRepository
{
    protected override SortingField DefaultSortingField { get; } = new("id");
    protected override string CollectionName => "rides";
    protected override string[] Columns => Ride.Columns;
}