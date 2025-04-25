using Ardalis.GuardClauses;
using Core.Enums;
using Core.Interfaces;
using Core.Models.Systems;
using Utils.Attributes;

namespace Core.Models;

public record Vehicle : IFormTableEntity, IDbEntity, IPrototype<Vehicle>
{
    public const int MaxNumber = 9999;
    
    [DbPrimaryKey]
    [UkrFormField("номер")]
    [DbColumn("number")]
    public int Number { get; private set; }
    
    [UkrFormField("тип")]
    [DbColumn("type")]
    public VehicleType Type { get; private set; }

    public Vehicle(int number, VehicleType type)
    {
        Guard.Against.NegativeOrZero(number, nameof(number));
        Number = number;
        Type = type;
    }

    [DbConstructor]
    public Vehicle(decimal number, short type) : this((int)number, (VehicleType)type) { }
    
    public string[] RowDisplayValues => [Number.ToString(), Type.ToString()];
    public string? UrlOnPage => null;
    
    public static readonly Dictionary<string, string> FieldNamesUkrToEngDictionary = new()
    {
        ["номер"] = "number",
        ["тип"] = "type"
    };

    public static readonly string[] FormFields = FieldNamesUkrToEngDictionary.Keys.ToArray();
    public static readonly string[] FormFieldEng = ["number", "type"];
    
    public string AsSqlRow() => $"{Number}, {(int)Type}";

    public static readonly string[] Columns = ["number", "type"];

    public static readonly Field[] SortingFields = [new(nameof(Number), "number", "номер")];
    public Vehicle DeepCopy() => new(Number, Type);
}