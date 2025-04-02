namespace DbCourseWork.Models;

public record CardOwnerCreateDto(string FirstName, string LastName, string? MiddleName, DateOnly BirthDate);