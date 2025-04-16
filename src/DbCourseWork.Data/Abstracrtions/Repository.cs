using System.Text;
using Core.Interfaces;
using Data.Context;
using Utils;

namespace Data.Abstracrtions;

public abstract class Repository<TEntity>(DataContext dataContext)
    : ReadOnlyRepository<TEntity>(dataContext), IRepository<TEntity> where TEntity : IDbEntity
{
    private readonly DataContext _dataContext = dataContext;
    protected abstract string[] Columns { get; }

    public Task InsertRange(IEnumerable<TEntity> entities)
    {
        var sb = InsertBuilder;
        foreach (var entity in entities)
            sb.AppendLine($"({entity.AsSqlRow()}),");

        var sql = sb.EndSql().ToString();
        return _dataContext.ExecuteSql(sql);
    }

    public Task Insert(TEntity entity)
    {
        var sql = InsertBuilder.AppendLine($"({entity.AsSqlRow()});").ToString();
        return _dataContext.ExecuteSql(sql);
    }

    private StringBuilder InsertBuilder =>
        new StringBuilder().AppendLine($"INSERT INTO {CollectionName} ({string.Join(", ", Columns)}) VALUES ");
}