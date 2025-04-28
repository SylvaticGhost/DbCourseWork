using Ardalis.Result;
using Core.Models.DTOs;
using Core.Validations;
using Data.Repositories;
using Services.Abstractions;
using Utils;
using ResultExtensions = Utils.ResultExtensions;
using Route = Core.Models.Route;

namespace Services;

public class RouteService(IRouteRepository repository)
    : MutableService<Route>(repository), IRouteService
{
    public Task<Route[]> GetAllRoutes() => repository.GetAllRoutes();

    public Task<Result<Route>> Find(string number) =>
        ResultExtensions.InErrorHandler(() => repository.GetRoute(number));


    public async Task<Result<Route>> Create(RouteCreateDto createDto)
    {
        var validation = Validator.Use<RouteCreateValidator, RouteCreateDto>(createDto);

        if (!validation.IsSuccess)
            return validation;

        var route = Route.Create(createDto);
        if (await repository.Exists(createDto.Number, createDto.Name))
            return Result<Route>.Conflict("Маршрут з такою назвою чи номером вже існує");

        await repository.SaveRoute(route);
        return route;
    }
}