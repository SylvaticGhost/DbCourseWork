using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Data.Context;

public class DataContext
{
    public static bool LogSql { get; set; } = true;

    private static string? _connectionString;

    private readonly NpgsqlConnection _connection;

    private bool _connectionOpened = false;

    public DataContext(IConfiguration configuration)
    {
        _connectionString ??= configuration["PgConnection"] ??
                              throw new ArgumentNullException(nameof(configuration), "Connection string not found");

        _connection = new NpgsqlConnection(_connectionString);
    }

    private static Task<T> InSqlLog<T>(Task<T> task, string sql)
    {
        if (LogSql)
        {
            Console.WriteLine(sql);
            Console.WriteLine();
        }    

        return task;
    }

    public Task<IEnumerable<T>> LoadData<T>(string sql) => InSqlLog(_connection.QueryAsync<T>(sql), sql);

    public Task<T> LoadDataSingle<T>(string sql) => InSqlLog(_connection.QuerySingleAsync<T>(sql), sql);

    public Task<bool> ExecuteSql(string sql) =>
        InSqlLog(_connection.ExecuteAsync(sql).ContinueWith(t => t.Result > 0), sql);

    public Task<bool> ExecuteSql(string sql, DynamicParameters parameters) =>
        InSqlLog(_connection.ExecuteAsync(sql, parameters).ContinueWith(t => t.Result > 0), sql);

    public Task<IEnumerable<T>> LoadData<T>(string sql, DynamicParameters parameters) =>
        InSqlLog(_connection.QueryAsync<T>(sql, parameters), sql);

    public Task<T> LoadDataSingle<T>(string sql, DynamicParameters parameters) =>
        InSqlLog(_connection.QuerySingleAsync<T>(sql, parameters), sql);

    public ValueTask<NpgsqlTransaction> BeginTransaction()
    {
        OpenConnection();
        return _connection.BeginTransactionAsync();
    }

    private void OpenConnection()
    {
        if(_connectionOpened)
            return;
        
        _connection.Open();
        _connectionOpened = true;
    }
}