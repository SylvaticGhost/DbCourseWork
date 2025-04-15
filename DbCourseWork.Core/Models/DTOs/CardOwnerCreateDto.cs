namespace Core.Models.DTOs;

public record CardOwnerCreateDto(string FirstName, string LastName, string? MiddleName, DateOnly BirthDate);