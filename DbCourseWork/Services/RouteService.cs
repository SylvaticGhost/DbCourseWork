using Ardalis.Result;
using DbCourseWork.Models;
using DbCourseWork.Repositories;

namespace DbCourseWork.Services;

public class RouteService(IRouteRepository repository) : IRouteService
{
    public Task<Models.Route[]> GetAllRoutes() => repository.GetAllRoutes();
    
    public async Task<Result<Models.Route>> Create(RouteCreateDto routeCreateDto)
    {
        if (await repository.Exists(routeCreateDto.Number, routeCreateDto.Name))
            return Result<Models.Route>.Conflict("Маршрут з такою назвою чи номером вже існує");
        
        var route = new Models.Route(routeCreateDto.Number, routeCreateDto.Name, routeCreateDto.Operator);
        await repository.SaveRoute(route);
        return route;
    }
}