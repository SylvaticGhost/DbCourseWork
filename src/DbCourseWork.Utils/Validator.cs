using Ardalis.Result;
using FluentValidation;
using FluentValidation.Results;

namespace Utils;

public static class Validator
{
    public static Result Use<TValidator, T>(T entity) where TValidator : AbstractValidator<T>, new()
    {
        var validator = new TValidator();
        var result = validator.Validate(entity);
        string errorMessage = string.Join(',', result.Errors.Select(e => e.ErrorMessage));
        if (result.IsValid)
            return Result.Success();
        
        var res =  Result.Invalid(new ValidationError(errorMessage));
        return res;
    } 
}