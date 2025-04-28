using Ardalis.GuardClauses;
using Core.Interfaces;
using Utils;
using Utils.Attributes;

namespace Core.Models;

public record Ride : IDbEntity, IFormTableEntity
{
    [UkrFormField("id")]
    [DbColumn("id")]
    public Guid Id { get; init; }

    [UkrFormField("номер ТЗ")]
    [DbColumn("vehicle")]
    public int Vehicle { get; init; }

    [UkrFormField("маршрут")]
    [DbColumn("route")]
    public string Route { get; init; }

    [DbConstructor]
    public Ride(Guid id, int vehicle, string route)
    {
        Guard.Against.Default(id, nameof(id));
        Guard.Against.NullOrEmpty(route, nameof(route));
        Guard.Against.Default(vehicle, nameof(vehicle));

        Id = id;
        Vehicle = vehicle;
        Route = route;
    }

    public static readonly string[] Columns = ["id", "vehicle", "route"];

    public string AsSqlRow() => $"'{Id}', {Vehicle}, '{LocalizationHelper.ToCyrillicLetters(Route)}'";
    
    public string[] RowDisplayValues => [Id.ToString(), Vehicle.ToString(), Route];
    public string? UrlOnPage => null;
}