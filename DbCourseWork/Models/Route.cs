using DbCourseWork.Models.Enums;
using DbCourseWork.Models.Primitives;

namespace DbCourseWork.Models;

public record Route : IFormTableEntity, IDbEntity
{
    public string Number { get; init; }
    
    public string Name { get; init; }
    
    public int Operator { get; init; }

    public Route(string number, string name, short @operator)
    {
        Number = number;
        Name = name;
        Operator = @operator;
    }

    public VehicleType Vehicle => VehicleMapper.GetFromAbbreviation(GetPrefix(Number));

    public static string GetPrefix(string number) => new (number.TakeWhile(char.IsLetter).ToArray());
    
    public string OperatorFullName => ((Operators)Operator).ToOfficialName();

    public string[] RowDisplayValues => [Number, Name, ((Operators)Operator).ToOfficialName(), OperatorFullName];
    public string? UrlOnPage => $"/RouteInfo/{Number}";

    public static readonly string[] FormFields = ["Номер", "Назва", "Оператор", "Вид транспорту"];
    public static readonly string[] Columns = ["number", "name", "operator", "vehicle"];
    public string AsSqlRow() => $"('{Number}', '{Name}', {Operator})";
}