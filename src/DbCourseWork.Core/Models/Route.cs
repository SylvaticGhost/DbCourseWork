using Ardalis.GuardClauses;
using Core.Enums;
using Core.Interfaces;
using Core.Models.DTOs;
using Core.Models.Systems;
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
        Guard.Against.NullOrEmpty(number, nameof(number));
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.Negative(@operator, nameof(@operator));

        Number = number;
        Name = name;
        Operator = @operator;
    }

    public static Route Create(RouteCreateDto dto) 
        => new(dto.Number, dto.Name, (short)dto.Operator);

    [UkrFormField("вид транспорту")]
    public VehicleType Vehicle => VehicleMapper.GetFromAbbreviation(GetPrefix(Number));

    public static string GetPrefix(string number) => new(number.TakeWhile(char.IsLetter).ToArray());

    public string OperatorFullName => ((Operators)Operator).ToOfficialName();

    public string[] RowDisplayValues => [Number, Name, ((Operators)Operator).ToOfficialName(), Vehicle.ToString()];
    public string? UrlOnPage => $"/RouteInfo/{Number}";

    public static readonly string[] FormFields = ["Номер", "Назва", "Оператор", "Вид транспорту"];
    public static readonly string[] Columns = ["number", "name", "operator"];
    public string AsSqlRow() => $"('{Number}', '{Name}', {Operator})";
}