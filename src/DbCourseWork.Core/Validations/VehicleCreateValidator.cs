using Core.Models;
using FluentValidation;

namespace Core.Validations;

public class VehicleCreateValidator : AbstractValidator<Vehicle>
{
    public VehicleCreateValidator() => RuleFor(x => x.Number).LessThanOrEqualTo(Vehicle.MaxNumber)
        .GreaterThan(0)
        .WithMessage("Номер повинен бути в діапазоні від 1 до 9999");
}