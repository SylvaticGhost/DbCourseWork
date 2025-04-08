namespace DbCourseWork.Models;

public record RideCardOperation : CardOperation
{
    public Ride? Ride { get; init; }

    public RideCardOperation(Guid id, int vehicle, string route, long card, DateTime date, int change) :
        base(card, id, date, change)
    {
        Ride = new Ride(id, vehicle, route);
    }
    
    public RideCardOperation(Guid id, int vehicle, string route, decimal card, DateTime date, int change)
        : this(id, vehicle, route, (long)card, date, change) { }
    
    public (Ride ride, CardOperation operation) ToRideAndOperation() => (Ride, this);
}