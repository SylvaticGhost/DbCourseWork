using Core.Enums;

namespace Core.Models.DTOs;

public record RouteCreateDto(string Number, string Name, Operators Operator);