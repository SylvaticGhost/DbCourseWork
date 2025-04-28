using Core.Enums;
using Core.Interfaces;

namespace Core.Models.DTOs;

public record VehicleUpsertParam(VehicleType Type, int Number) : IUpsertParam<Vehicle>
{
    public VehicleType Type { get; set; } = Type;

    public int Number { get; set; } = Number;
    
    public static VehicleUpsertParam Empty() => new(VehicleType.Автобус, 0);
    
    public Vehicle ToCreateDto() => new (Number, Type);

    public void Clear()
    {
        Type = VehicleType.Автобус;
        Number = 0;
    }
}