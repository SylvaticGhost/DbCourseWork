using Ardalis.Result;
using Core.Models.DTOs;
using Services.Abstractions;
using Route = Core.Models.Route;

namespace Services;

public interface IRouteService : IMutableService<Route>
{
    public Task<Route[]> GetAllRoutes();

    public Task<Result<Route>> Create(RouteCreateDto createDto);

    public Task<Result<Route>> Find(string number);
}