using Core.Interfaces;

namespace Core.Models;

public record CardOwner : IDbEntity, IFormTableEntity
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
        RowDisplayValues =
        [
            id.ToString(),
            firstName,
            lastName,
            middleName ?? "не вказано",
            birthDate.ToString("dd.MM.yyyy")
        ];
    }

    public CardOwner(int id, string first_name, string last_name, string? middle_name, DateTime birth_date) : this(id,
        first_name, last_name, middle_name, DateOnly.FromDateTime(birth_date)) { }

    public string UrlOnPage => $"/User/{Id}";
    public string AsSqlRow() => $"{Id}, '{FirstName}', '{LastName}', '{MiddleName}', '{BirthDate}'";

    public static readonly string[] Columns = ["id", "first_name", "last_name", "middle_name", "birth_date"];

    public static readonly string[] FormFields = ["id", "імя", "прізвище", "по батькові", "дата народження"];

    public string[] RowDisplayValues { get; }
}