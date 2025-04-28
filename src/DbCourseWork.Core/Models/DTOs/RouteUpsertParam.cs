using Core.Enums;
using Core.Interfaces;

namespace Core.Models.DTOs;

public record RouteUpsertParam(string NumberPrefix, short NumberSuffix, string Name, Operators Operator)
    : IPrototype<RouteUpsertParam>, IUpsertParam<RouteCreateDto>
{
    public string Number => $"{NumberPrefix}{NumberSuffix}";
    public string NumberPrefix { get; set; } = NumberPrefix;
    public short NumberSuffix { get; set; } = NumberSuffix;

    public string Name { get; set; } = Name;
    public Operators Operator { get; set; } = Operator;

    public static RouteUpsertParam Empty() => new(string.Empty, 0, string.Empty, Operators.KpKyivMetropoliten);

    public RouteUpsertParam DeepCopy() => new(NumberPrefix, NumberSuffix, Name, Operator);
    
    public RouteCreateDto ToCreateDto() => new(Number, Name, Operator);
    
    public void Clear() 
    {
        NumberPrefix = string.Empty;
        NumberSuffix = 0;
        Name = string.Empty;
        Operator = Operators.KpKyivMetropoliten;
    }
}