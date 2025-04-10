using DbCourseWork.Models.Primitives;

namespace DbCourseWork.Models;

public record TravelCard : IFormTableEntity, IDbEntity
{
    public long Code { get; private set; }

    public int OwnerId { get; private set; }

    public DateOnly ReleaseDate { get; private set; }

    public DateOnly ExpirationDate { get; private set; }

    public string CodeAsString => Code.ToString("D10");

    public TravelCard(long code, int ownerId, DateOnly releaseDate, DateOnly expirationDate)
    {
        Code = code;
        OwnerId = ownerId;
        ReleaseDate = releaseDate;
        ExpirationDate = expirationDate;
        RowDisplayValues =
        [
            CodeAsString, OwnerId.ToString(), ReleaseDate.ToString("yyyy MMMM dd"),
            ExpirationDate.ToString("yyyy MMMM dd")
        ];
    }

    public TravelCard(decimal code, int owner, DateTime release_date, DateTime expiration_date) : this((long)code,
        owner, DateOnly.FromDateTime(release_date), DateOnly.FromDateTime(expiration_date)) { }

    public static readonly string[] FormFields = ["Код", "Власник", "Дата випуску", "Дата кінця"];
    public string[] RowDisplayValues { get; }
    public string? UrlOnPage => null;
    public string AsSqlRow() => $"{Code}, {OwnerId}, '{ReleaseDate}', '{ExpirationDate}'";

    public static readonly string[] Columns = ["code", "owner", "release_date", "expiration_date"];
    
    public static readonly SortingField DefaultSortField = new("code");
}