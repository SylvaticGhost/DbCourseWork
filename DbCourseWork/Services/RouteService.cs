using Ardalis.Result;
using DbCourseWork.Models;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;
using Route = DbCourseWork.Models.Route;

namespace DbCourseWork.Services;

public class RouteService(IRouteRepository repository) : IRouteService
{
    public Task<Route[]> GetAllRoutes() => repository.GetAllRoutes();

    public Task<Result<Route>> Find(string number) =>
        ResultExtensions.InErrorHandler(() => repository.GetRoute(number));

    
    public async Task<Result<Route>> Create(RouteCreateDto routeCreateDto)
    {
        if (await repository.Exists(routeCreateDto.Number, routeCreateDto.Name))
            return Result<Route>.Conflict("Маршрут з такою назвою чи номером вже існує");
        
        var route = new Route(routeCreateDto.Number, routeCreateDto.Name, routeCreateDto.Operator);
        await repository.SaveRoute(route);
        return route;
    }
}