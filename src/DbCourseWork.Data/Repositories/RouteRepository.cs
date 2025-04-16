using Core.Models.Systems;
using Dapper;
using Data.Abstracrtions;
using Data.Context;
using Data.Utils;
using Route = Core.Models.Route;

namespace Data.Repositories;

public class RouteRepository(DataContext dataContext) : Repository<Route>(dataContext),IRouteRepository
{
    private readonly DataContext _dataContext = dataContext;

    public Task<Route[]> GetAllRoutes()
    {
        const string sql = "SELECT * FROM routes";
        return _dataContext.LoadData<Route>(sql).ContinueWith(t => t.Result.ToArray());
    }
    
    public Task SaveRoute(Route route) 
    {
        const string sql = "INSERT INTO routes (number, name,operator) VALUES (@Number, @Name, @Operator)";
        var parameters = new DynamicParameters();
        parameters.Add("Number", route.Number);
        parameters.Add("Name", route.Name);
        parameters.Add("Operator", route.Operator);
        return _dataContext.ExecuteSql(sql, parameters);
    }
    
    public Task<bool> Exists(string number, string name) 
    {
        const string sql = "SELECT COUNT(*) > 1 FROM routes WHERE number = @Number OR name = @Name";
        var parameters = new DynamicParameters();
        parameters.Add("Number", number);
        parameters.Add("Name", name);
        return _dataContext.LoadDataSingle<bool>(sql, parameters);
    }

    public Task<Route> GetRoute(string number)
    {
        const string sql = "SELECT * FROM routes WHERE number = @Number";
        var parameters = DynamicParametersExtensions.WithSingleParameter("@Number", number);
        return _dataContext.LoadDataSingle<Route>(sql, parameters);
    }

    protected override SortingField DefaultSortingField => new("number");
    protected override string CollectionName => "routes";
    protected override string[] Columns => Route.Columns;
}