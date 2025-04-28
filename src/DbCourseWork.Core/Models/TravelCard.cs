using Core.Interfaces;
using Core.Models.Systems;
using Utils.Attributes;
using Utils.Extensions;

namespace Core.Models;

public record TravelCard : IFormTableEntity, IDbEntity
{
    [UkrFormField("Код")]
    [DbColumn("code")]
    public long Code { get; private set; }

    [UkrFormField("Власник")]
    [DbColumn("owner")]
    public int OwnerId { get; private set; }

    [UkrFormField("Дата випуску")]
    [DbColumn("release_date")]
    public DateOnly ReleaseDate { get; private set; }
    
    [UkrFormField("Дата кінця")]
    [DbColumn("expiration_date")]
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
            CodeAsString, OwnerId.ToString(), ReleaseDate.ToUkr(),
            ExpirationDate.ToUkr()
        ];
    }

    public TravelCard(decimal code, int owner, DateTime release_date, DateTime expiration_date) : this((long)code,
        owner, DateOnly.FromDateTime(release_date), DateOnly.FromDateTime(expiration_date)) { }

    public string[] RowDisplayValues { get; }
    public string? UrlOnPage => null;
    public string AsSqlRow() => $"{Code}, {OwnerId}, '{ReleaseDate}', '{ExpirationDate}'";

    public static readonly string[] Columns = ["code", "owner", "release_date", "expiration_date"];
    
    public static readonly SortingField DefaultSortField = new("code");
}