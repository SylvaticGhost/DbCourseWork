using Npgsql;

namespace DbCourseWork.Data;

public static class ConnectionExtensions
{
    public static bool ExecuteSqlWithParameters(this NpgsqlConnection dbConnection,string sql, List<NpgsqlParameter> parameters)
    {
        NpgsqlCommand commandWithParams = new();

        foreach (var parameter in parameters)
            commandWithParams.Parameters.Add(parameter);
        
        commandWithParams.Connection = dbConnection;
        commandWithParams.CommandText = sql;
        int rowsAffected = commandWithParams.ExecuteNonQuery();
        
        return rowsAffected > 0;
    }
}