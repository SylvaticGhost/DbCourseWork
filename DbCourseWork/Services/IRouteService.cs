using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface IRouteService
{
    public Task<Models.Route[]> GetAllRoutes();

    public Task<Result<Models.Route>> Create(RouteCreateDto routeCreateDto);
}