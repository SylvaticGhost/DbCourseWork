using Dapper;
using Npgsql;

namespace DbCourseWork.Data;

public class DataContext
{
    private static string? _connectionString;
    
    private readonly NpgsqlConnection _connection;
    
    public DataContext(IConfiguration configuration)
    {
        _connectionString ??= configuration["PgConnection"]
                                 ?? throw new ArgumentNullException(nameof(configuration),"Connection string not found");
        
        _connection = new NpgsqlConnection(_connectionString);
    }

    public async Task<TResult> InTransaction<TResult>(Func<NpgsqlConnection, Task<TResult>> action)
    {
        var transaction = await _connection.BeginTransactionAsync();
        try
        {
            var result = await action(_connection);
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    public Task<IEnumerable<T>> LoadData<T>(string sql) => _connection.QueryAsync<T>(sql);

    public Task<T> LoadDataSingle<T>(string sql) => _connection.QuerySingleAsync<T>(sql);

    public Task<bool> ExecuteSql(string sql) => _connection.ExecuteAsync(sql).ContinueWith(t => t.Result > 0);

    public Task<int> ExecuteSqlWithRowCount(string sql) => _connection.ExecuteAsync(sql);
    
    public Task<bool> ExecuteSql(string sql, DynamicParameters parameters) => _connection.ExecuteAsync(sql, parameters).ContinueWith(t => t.Result > 0);

    public Task<IEnumerable<T>> LoadData<T>(string sql, DynamicParameters parameters) => _connection.QueryAsync<T>(sql, parameters);

    public Task<T> LoadDataSingle<T>(string sql, DynamicParameters parameters) => _connection.QuerySingleAsync<T>(sql, parameters);
}
