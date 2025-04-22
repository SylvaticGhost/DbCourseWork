using System.Reflection;
using System.Text;
using Core.Interfaces;
using Data.Context;
using Utils;
using Utils.Attributes;

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

    public Task Delete(TEntity entity)
    {
        PropertyInfo[] keyProperties = PropertyMapper.GetByAttribute<DbPrimaryKeyAttribute>(typeof(TEntity));
        
        if (keyProperties.Length == 0)
            throw new InvalidOperationException($"Type {typeof(TEntity).Name} does not have a primary key.");
        
        object?[] keyValues = keyProperties.Select(prop => prop.GetValue(entity)).ToArray();
        
        var sql = $"DELETE FROM {CollectionName} WHERE {string.Join(" AND ", keyProperties.Select((prop, i) => $"{prop.Name} = {keyValues[i]}"))};";
        return _dataContext.ExecuteSql(sql);
    }

    // public Task Update(TEntity entity)
    // {
    //     
    // }

    private StringBuilder InsertBuilder =>
        new StringBuilder().AppendLine($"INSERT INTO {CollectionName} ({string.Join(", ", Columns)}) VALUES ");
    
    // private StringBuilder UpdateBuilder =>
    //     new StringBuilder().AppendLine($"UPDATE {CollectionName} SET ");
}