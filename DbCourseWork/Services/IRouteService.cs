using Ardalis.Result;
using DbCourseWork.Models;
using Route = DbCourseWork.Models.Route;

namespace DbCourseWork.Services;

public interface IRouteService : ISearchableService<Route>
{
    public Task<Route[]> GetAllRoutes();

    public Task<Result<Route>> Create(RouteCreateDto routeCreateDto);

    public Task<Result<Route>> Find(string number);
}