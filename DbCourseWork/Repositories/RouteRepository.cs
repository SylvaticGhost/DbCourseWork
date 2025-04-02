using Dapper;
using DbCourseWork.Data;

namespace DbCourseWork.Repositories;

public class RouteRepository(DataContext dataContext) : IRouteRepository
{
    public Task<Models.Route[]> GetAllRoutes()
    {
        const string sql = "SELECT * FROM routes";
        return dataContext.LoadData<Models.Route>(sql).ContinueWith(t => t.Result.ToArray());
    }
    
    public Task SaveRoute(Models.Route route) 
    {
        const string sql = "INSERT INTO routes (number, name,operator) VALUES (@Number, @Name, @Operator)";
        var parameters = new DynamicParameters();
        parameters.Add("Number", route.Number);
        parameters.Add("Name", route.Name);
        parameters.Add("Operator", route.Operator);
        return dataContext.ExecuteSql(sql, parameters);
    }
    
    public Task<bool> Exists(string number, string name) 
    {
        const string sql = "SELECT COUNT(*) > 1 FROM routes WHERE number = @Number OR name = @Name";
        var parameters = new DynamicParameters();
        parameters.Add("Number", number);
        parameters.Add("Name", name);
        return dataContext.LoadDataSingle<bool>(sql, parameters);
    }
}