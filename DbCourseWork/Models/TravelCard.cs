namespace DbCourseWork.Models;

public record TravelCard
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
    }
    
    public TravelCard(decimal code, int owner, DateTime release_date, DateTime expiration_date)
        : this((long)code, owner, DateOnly.FromDateTime(release_date), DateOnly.FromDateTime(expiration_date)) {}
}