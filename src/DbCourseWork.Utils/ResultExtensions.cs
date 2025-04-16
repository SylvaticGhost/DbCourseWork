using Ardalis.Result;

namespace Utils;

public static class ResultExtensions
{
    public static async Task<Result<T>> InErrorHandler<T>(Func<Task<T>> func, bool throwInDebug = false)
    {
        try
        {
            var result = await func();
            
            if(result is null)
                return Result<T>.NotFound();
            
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            if (throwInDebug)
                throw;
            return Result<T>.Error(ex.Message);
        }
    }
    
    public static async Task<Result<T>> InErrorHandler<T>(Task<T> task, bool throwInDebug = false)
    {
        try
        {
            var result = await task;
            
            if(result is null)
                return Result<T>.NotFound();
            
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            if (throwInDebug)
                throw;
            return Result<T>.Error(ex.Message);
        }
    }
    
    public static string JoinErrorMessage<T>(this Result<T> result) => string.Join(',', result.Errors);
}