using System.Reflection;
using Ardalis.Result;
using Core.Interfaces;

namespace Services;

public class EditableService<TEntity> where TEntity : class, IDbEntity, IPrototype<TEntity>
{
    public Task<Result<TEntity>> Edit(TEntity entity, Action<TEntity> updateAction)
    {
        var v1 = entity.DeepCopy();
        updateAction(entity);
        var v1PropsWithValues = GetPropertiesWithValues(v1);
        var v2PropsWithValues = GetPropertiesWithValues(entity);
        var difference = GetDifferent(v1PropsWithValues, v2PropsWithValues);
        
        if (difference.Count == 0)
            return Task.FromResult(Result<TEntity>.Success(entity));

        throw new NotImplementedException();
    }

    private record Prop(PropertyInfo Property, object? Value);
    
    private static Dictionary<string, Prop> GetPropertiesWithValues(object obj) => obj.GetType().GetProperties()
        .Where(p => p is { CanRead: true, CanWrite: true })
        .Select(p => new Prop(p, p.GetValue(obj)))
        .ToDictionary(p => p.Property.Name, p => p);
    
    private static List<Prop> GetDifferent(Dictionary<string, Prop> v1, Dictionary<string, Prop> v2)
    {
        var result = new List<Prop>();
        foreach ((string key, var prop) in v1)
        {
            if (!v2.TryGetValue(key, out var prop2))
                continue;

            if (prop.Value != prop2.Value)
                result.Add(prop);
        }

        return result;
    }
}