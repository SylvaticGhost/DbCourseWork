using Core.Enums;
using Core.Interfaces;
using Utils.Attributes;

namespace Core.Models;

public record CardOperation : IDbEntity
{
    [DbPrimaryKey]
    [DbColumn("card")]
    public long Card { get; private set; }
    
    [DbColumn("ride")]
    public Guid Ride { get; private set; }
    
    [DbPrimaryKey]
    [DbColumn("date")]
    public DateTime Date { get; private set; }
    
    [DbColumn("change")]
    public int Change { get; private set; }
    
    public CardOperationType Type => Change < 0 ? CardOperationType.Travel : CardOperationType.Replenishment;

    public CardOperation(long card, Guid ride, DateTime date, int change)
    {
        Card = card;
        Ride = ride;
        Date = date;
        Change = change;
    }

    public string AsSqlRow() => $"{Card}, '{Date}', {Change}, '{Ride}'  ";
    
    public static readonly string[] Columns = ["card", "date", "change", "ride"];
}