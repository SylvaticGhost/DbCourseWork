namespace DbCourseWork.Models;

public record CardOwner
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    
    public CardOwner(int id, string firstName, string lastName, string? middleName, DateOnly birthDate)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        BirthDate = birthDate;
    }

    public CardOwner(int id, string first_name, string last_name, string? middle_name, DateTime birth_date) 
        : this(id, first_name, last_name, middle_name, DateOnly.FromDateTime(birth_date)) { }
    
    public string UrlOnPage => $"/User/{Id}";
}