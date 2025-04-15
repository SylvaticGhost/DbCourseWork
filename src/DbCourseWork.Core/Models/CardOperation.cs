using Core.Enums;
using Core.Interfaces;

namespace Core.Models;

public record CardOperation : IDbEntity
{
    public long Card { get; private set; }
    
    public Guid Ride { get; private set; }
    
    public DateTime Date { get; private set; }
    
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