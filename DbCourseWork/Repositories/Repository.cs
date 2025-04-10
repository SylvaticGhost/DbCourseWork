using System.Text;
using DbCourseWork.Data;
using DbCourseWork.Models.Primitives;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public abstract class Repository<TEntity>(DataContext dataContext) : ReadOnlyRepository<TEntity>(dataContext) where TEntity : IDbEntity
{
    private readonly DataContext _dataContext = dataContext;
    protected abstract string[] Columns { get; }

    public Task InsertRange(IEnumerable<TEntity> entities) 
    {
        var sb = new StringBuilder().AppendLine($"INSERT INTO {CollectionName} ({string.Join(", ", Columns)}) VALUES ");
        foreach (var entity in entities)
            sb.AppendLine($"({entity.AsSqlRow()}),");

        var sql = sb.EndSql().ToString();
        return _dataContext.ExecuteSql(sql);
    }
}