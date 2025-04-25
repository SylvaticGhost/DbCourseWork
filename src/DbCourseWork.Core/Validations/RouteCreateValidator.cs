using Core.Models;
using Core.Models.DTOs;
using Core.Models.Systems;
using FluentValidation;

namespace Core.Validations;

public class RouteCreateValidator : AbstractValidator<RouteCreateDto>
{
    public RouteCreateValidator()
    {
        RuleFor(r => r.Number).Matches(RouteNumber.RegexPattern)
            .WithMessage("Номер маршруту не відповідає формату.");
        RuleFor(r => r.Name).NotEmpty().WithMessage("Назва маршруту не може бути порожньою.");
        RuleFor(r => r.Operator).IsInEnum()
            .WithMessage("Оператор маршруту не відповідає жодному з відомих операторів.");
    }
}