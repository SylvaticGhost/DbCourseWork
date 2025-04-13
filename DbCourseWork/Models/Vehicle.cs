using DbCourseWork.Models.Enums;
using DbCourseWork.Models.Primitives;

namespace DbCourseWork.Models;

public record Vehicle : IFormTableEntity, IDbEntity
{
    public int Number { get; private set; }

    public VehicleType Type { get; private set; }

    public Vehicle(int number, VehicleType type)
    {
        Number = number;
        Type = type;
    }

    public Vehicle(decimal number, short type) : this((int)number, (VehicleType)type) { }
    public string[] RowDisplayValues => [Number.ToString(), Type.ToString()];
    public string? UrlOnPage => null;

    public static readonly string[] FormFields = ["номер", "тип"];
    public string AsSqlRow() => $"({Number}, {(int)Type})";

    public static readonly string[] Columns = ["number", "type"];

    public static readonly Field[] SortingFields = [new(nameof(Number), "number", "номер")];
}