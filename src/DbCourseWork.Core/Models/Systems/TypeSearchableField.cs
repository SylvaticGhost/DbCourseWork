using System.Collections;
using Core.Interfaces;

namespace Core.Models.Systems;

public abstract record TypeSearchableField
{
    public abstract Type Type { get; }

    public abstract string GetSqlCondition(params string[] displayNames);
    
    public abstract string[] Values { get; }
    
    public required string DisplayName { get; init; } 
    
    public required string Column { get; init; } 
}

public record TypeSearchableField<TEnum, TEntity> : TypeSearchableField where TEnum : Enum where TEntity : IDbEntity
{
    public override Type Type => typeof(TEnum);
    
    public TEnum[] EnumValues => Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
    public override string[] Values { get; }

    public required Func<TEntity, TEnum> Getter { get; init; } 
    public required Func<TEnum, string> ToDbValue { get; init; } 
    public required Func<TEnum, string>? ToDisplayName { get; init; }
    
    private Dictionary<TEnum, string> DisplayNames { get; } = new(); 

    public TypeSearchableField()
    {
        ToDisplayName ??= @enum => @enum.ToString();
        foreach (var value in EnumValues)
        {
            var displayName = ToDisplayName(value);
            DisplayNames[value] = displayName;
        }
    }

    public override string GetSqlCondition(params string[] displayNames)
    {
        IEnumerable<TEnum> enums = GetFromDisplayName(displayNames);
        IEnumerable<string> dbValues = enums.Select(ToDbValue);
        string[] conditions = dbValues.Select(dbValue => $"{Column} = '{dbValue}'").ToArray();
        
        return conditions.Length == 1 ? conditions.First() : $"({string.Join(" OR ", conditions)})";
    }
    
    private IEnumerable<TEnum> GetFromDisplayName(params string[] displayNames) => 
        from pair in DisplayNames
        where displayNames.Contains(pair.Value)
        select pair.Key;
}
