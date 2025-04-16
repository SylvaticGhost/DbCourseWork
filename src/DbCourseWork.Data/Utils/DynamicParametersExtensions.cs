using Dapper;

namespace Data.Utils;

public class DynamicParametersExtensions
{
    public static DynamicParameters WithSingleParameter(string name, object value) 
    {
        var parameters = new DynamicParameters();
        parameters.Add(name, value);
        return parameters;
    }

    public static DynamicParameters Pagination(int page, int pageSize)
    {
        var parameters = new DynamicParameters();
        parameters.Add("pageSize", pageSize);
        parameters.Add("offset", (page - 1) * pageSize);
        return parameters;
    }
}