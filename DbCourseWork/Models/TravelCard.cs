namespace DbCourseWork.Models;

public record TravelCard
{
    public long Code { get; private set; }
    
    public int OwnerId { get; private set; }
    
    public DateOnly ReleaseDate { get; private set; }
    
    public DateOnly ExpirationDate { get; private set; }
}