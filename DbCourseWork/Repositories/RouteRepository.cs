using Dapper;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Utils;
using Route = DbCourseWork.Models.Route;

namespace DbCourseWork.Repositories;

public class RouteRepository(DataContext dataContext) : Repository<Route>(dataContext),IRouteRepository
{
    private readonly DataContext _dataContext = dataContext;

    public Task<Models.Route[]> GetAllRoutes()
    {
        const string sql = "SELECT * FROM routes";
        return _dataContext.LoadData<Models.Route>(sql).ContinueWith(t => t.Result.ToArray());
    }
    
    public Task SaveRoute(Models.Route route) 
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

    public Task<Models.Route> GetRoute(string number)
    {
        const string sql = "SELECT * FROM routes WHERE number = @Number";
        var parameters = DynamicParametersExtensions.WithSingleParameter("@Number", number);
        return _dataContext.LoadDataSingle<Models.Route>(sql, parameters);
    }

    protected override SortingField DefaultSortingField => new("number");
    protected override string CollectionName => "routes";
    protected override string[] Columns => Route.Columns;
}