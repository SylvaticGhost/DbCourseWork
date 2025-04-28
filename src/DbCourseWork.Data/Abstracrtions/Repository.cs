using System.Reflection;
using System.Text;
using Core.Interfaces;
using Data.Context;
using Data.Utils;
using Utils;
using Utils.Attributes;
using Utils.Extensions;

namespace Data.Abstracrtions;

public abstract class Repository<TEntity>(DataContext dataContext)
    : ReadOnlyRepository<TEntity>(dataContext), IRepository<TEntity> where TEntity : IDbEntity
{
    private readonly DataContext _dataContext = dataContext;
    
    protected static readonly string[] Columns = PropertyMapper.GetDbColumnNames(typeof(TEntity));

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

    public Task Delete(TEntity entity) =>
        _dataContext.ExecuteSql($"DELETE FROM {CollectionName} WHERE {GetEntityIdentity(entity)};");

    private StringBuilder InsertBuilder =>
        new StringBuilder().AppendLine($"INSERT INTO {CollectionName} ({string.Join(", ", Columns)}) VALUES ");

    public Task Update(TEntity oldEntity, TEntity newEntity)
    {
        PropertyInfo[] properties = GetDbProperties(oldEntity);
        var sb = new StringBuilder().AppendLine($"UPDATE {CollectionName} SET ");
        for (var i = 0; i < properties.Length; i++)
        {
            var prop = properties[i];
            object? value = prop.GetValue(newEntity);
            if (prop.GetValue(oldEntity) != prop.GetValue(newEntity))
                sb.Append($"{prop.Name} = {SqlTextHelper.FormatValue(value)}");
            
            if (i != properties.Length - 1)
                sb.Append(',');
            
            sb.Append('\n');
        }

        sb.AppendLine($"WHERE {GetEntityIdentity(oldEntity)};");
        var sql = sb.ToString();
        return _dataContext.ExecuteSql(sql);
    }

    private static string GetEntityIdentity(TEntity entity)
    {
        PropertyInfo[] keyProperties = PropertyMapper.GetByAttribute<DbPrimaryKeyAttribute>(typeof(TEntity));
        if (keyProperties.Length == 0)
            keyProperties = GetDbProperties(entity);

        object?[] keyValues = keyProperties.Select(prop => prop.GetValue(entity)).ToArray();
        return string.Join(" AND ",
            keyProperties.Select((prop, i) => $"{prop.Name} = {SqlTextHelper.FormatValue(keyValues[i])}"));
    }

    private static PropertyInfo[] GetDbProperties(TEntity entity)
    {
        PropertyInfo[] keyProperties = PropertyMapper.GetByAttribute<DbColumnAttribute>(typeof(TEntity));
        if (keyProperties.Length == 0)
            throw new InvalidOperationException($"Type {typeof(TEntity).Name} does not have a primary key.");
        return keyProperties;
    }
}