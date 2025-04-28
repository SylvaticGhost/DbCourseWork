using Ardalis.Result;
using FluentValidation.Results;

namespace Utils.Extensions;

public static class ResultExtensions
{
    public static Task<Result<T>> InErrorHandler<T>(Func<Task<T>> func, bool throwInDebug = false) =>
        InErrorHandler(func(), throwInDebug);

    public static async Task<Result<T>> InErrorHandler<T>(Task<T> task, bool throwInDebug = false)
    {
        try
        {
            var result = await task;

            if (result is null)
                return Result<T>.NotFound();

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return FailedExit(ex, throwInDebug);
        }
    }

    public static async Task<Result<T>> InErrorHandler<T>(Task task, Result<T> result, bool throwInDebug = false)
    {
        try
        {
            await task;
            return result;
        }
        catch (Exception ex)
        {
            return FailedExit(ex, throwInDebug);
        }
    }

    public static async Task<Result> InErrorHandler(Task task, bool throwInDebug = false)
    {
        try
        {
            await task;
            return Result.Success();
        }
        catch (Exception ex)
        {
            return FailedExit(ex, throwInDebug);
        }
    }

    private static Result FailedExit(Exception ex, bool throwInDebug)
    {
        if (throwInDebug)
            throw ex;
        return Result.Error(ex.Message);
    }
    
    

    public static string JoinErrorMessage<T>(this Result<T> result) => string.Join(',', result.Errors);

    public static Result FromValidationResult(ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Result.Success();

        var errors = validationResult.Errors;
        var errorMessages = errors.Select(e => e.ErrorMessage).ToArray();
        var validationError = new ValidationError(string.Join(',', errorMessages));
        return Result.Invalid(validationError);
    }
}