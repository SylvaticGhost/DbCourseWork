using Ardalis.Result;
using Core.Models.DTOs;
using Core.Validations;
using Data.Repositories;
using Services.Abstractions;
using Utils;
using ResultExtensions = Utils.Extensions.ResultExtensions;
using Route = Core.Models.Route;

namespace Services;

public class RouteService(IRouteRepository repository)
    : MutableService<Route, RouteCreateDto, RouteCreateValidator>(repository), IRouteService
{
    public Task<Result<Route>> Find(string number) =>
        ResultExtensions.InErrorHandler(() => repository.GetRoute(number));

    protected override Route FabricMethod(RouteCreateDto createDto) => Route.Create(createDto);

    protected override async Task<Result<Route>> CreateChecks(Route entity)
    {
        if (await repository.Exists(entity.Number, entity.Name))
            return Result<Route>.Conflict("Маршрут з такою назвою чи номером вже існує");
        
        return Result<Route>.Success(entity);
    }
}