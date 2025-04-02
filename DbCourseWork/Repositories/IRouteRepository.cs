namespace DbCourseWork.Repositories;

public interface IRouteRepository
{
    public Task<Models.Route[]> GetAllRoutes();

    public Task SaveRoute(Models.Route route);

    public Task<bool> Exists(string number, string name);
}