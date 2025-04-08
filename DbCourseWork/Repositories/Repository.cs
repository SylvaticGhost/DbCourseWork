using System.Text;
using DbCourseWork.Data;
using DbCourseWork.Models.Primitives;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public abstract class Repository<TEntity>(DataContext dataContext) where TEntity : IInsertableEntity
{
    protected abstract string CollectionName { get; }
    protected abstract string[] Columns { get; }

    public Task InsertRange(IEnumerable<TEntity> entities) 
    {
        var sb = new StringBuilder().AppendLine($"INSERT INTO {CollectionName} ({string.Join(", ", Columns)}) VALUES ");
        foreach (var entity in entities)
            sb.AppendLine($"({entity.AsSqlRow()}),");

        var sql = sb.EndSql().ToString();
        return dataContext.ExecuteSql(sql);
    }
}