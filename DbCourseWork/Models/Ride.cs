using Ardalis.GuardClauses;
using DbCourseWork.Helpers;
using DbCourseWork.Models.Primitives;

namespace DbCourseWork.Models;

public record Ride : IDbEntity, IFormTableEntity
{
    public Guid Id { get; init; }

    public int Vehicle { get; init; }

    public string Route { get; init; }

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

    public static readonly string[] FormFields = ["id", "номер ТЗ", "маршрут"];

    public string AsSqlRow() => $"'{Id}', {Vehicle}, '{LocalizationHelper.ToCyrillicLetters(Route)}'";
    
    public string[] RowDisplayValues => [Id.ToString(), Vehicle.ToString(), Route];
    public string? UrlOnPage => null;
}