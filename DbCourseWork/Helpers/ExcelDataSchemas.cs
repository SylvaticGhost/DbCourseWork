using DbCourseWork.Models;

namespace DbCourseWork.Helpers;

public class ExcelDataSchemas
{
    public static readonly IReadOnlyDictionary<byte, Field> RideSchema = new Dictionary<byte, Field>()
    {
        { 1, new Field(nameof(Ride.Id), typeof(Guid)) },
        { 2, new Field(nameof(Ride.Vehicle), typeof(int)) },
        { 3, new Field(nameof(Ride.Route), typeof(string)) },
    };
    
    public static readonly IReadOnlyDictionary<byte, Field> CardOperationSchema = new Dictionary<byte, Field>()
    {
        { 1, new Field(nameof(CardOperation.Card), typeof(long)) },
        { 2, new Field(nameof(CardOperation.Ride), typeof(Guid)) },
        { 3, new Field(nameof(CardOperation.Date), typeof(DateTime)) },
        { 4, new Field(nameof(CardOperation.Change), typeof(int)) },
    };
    
    public static readonly IReadOnlyDictionary<byte, Field> CardSchema = new Dictionary<byte, Field>()
    {
        { 1, new Field(nameof(TravelCard.Code), typeof(long)) },
        { 2, new Field(nameof(TravelCard.OwnerId), typeof(int)) },
        { 3, new Field(nameof(TravelCard.ReleaseDate), typeof(DateOnly)) },
        { 4, new Field(nameof(TravelCard.ExpirationDate), typeof(DateOnly)) },
    };
    
    
}