using Core.Enums;
using Core.Interfaces;
using Utils.Attributes;

namespace Core.Models;

public record Route : IFormTableEntity, IDbEntity
{
    [UkrFormField("номер")]
    [DbColumn("number")]
    public string Number { get; init; }
    
    [UkrFormField("назва")]
    [DbColumn("name")]
    public string Name { get; init; }
    
    [DbColumn("operator")]
    public int Operator { get; init; }
    
    [UkrFormField("оператор")]
    public Operators OperatorEnum => (Operators)Operator;

    public Route(string number, string name, short @operator)
    {
        Number = number;
        Name = name;
        Operator = @operator;
    }
    
    [UkrFormField("вид транспорту")]
    public VehicleType Vehicle => VehicleMapper.GetFromAbbreviation(GetPrefix(Number));

    public static string GetPrefix(string number) => new (number.TakeWhile(char.IsLetter).ToArray());
    
    public string OperatorFullName => ((Operators)Operator).ToOfficialName();

    public string[] RowDisplayValues => [Number, Name, ((Operators)Operator).ToOfficialName(), OperatorFullName];
    public string? UrlOnPage => $"/RouteInfo/{Number}";

    public static readonly string[] FormFields = ["Номер", "Назва", "Оператор", "Вид транспорту"];
    public static readonly string[] Columns = ["number", "name", "operator"];
    public string AsSqlRow() => $"('{Number}', '{Name}', {Operator})";
}