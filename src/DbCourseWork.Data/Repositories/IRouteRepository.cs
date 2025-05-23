using Data.Abstracrtions;
using Route = Core.Models.Route;

namespace Data.Repositories;

public interface IRouteRepository : IRepository<Route>
{
    public Task<Route[]> GetAllRoutes();

    public Task SaveRoute(Route route);

    public Task<bool> Exists(string number, string name);

    public Task<Route> GetRoute(string number);
}