using Dapper;

namespace DbCourseWork.Utils;

public class DynamicParametersExtensions
{
    public static DynamicParameters WithSingleParameter(string name, object value) 
    {
        var parameters = new DynamicParameters();
        parameters.Add(name, value);
        return parameters;
    }
}