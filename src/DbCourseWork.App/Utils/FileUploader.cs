using Ardalis.Result;

namespace WebUI.Utils;

public class FileUploader
{
    public static async Task<Result<List<T>>> OnFileUpload<T>(FileUploadEventArgs e, Func<Stream, int, Result<List<T>>> reader,
        Action? callback = null)
    {
        Result<List<T>> res;
        try
        {
            using var result = new MemoryStream();
            await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);
            res = reader(result, 1);
        }
        catch (Exception exc)
        {
            res = Result<List<T>>.Error(exc.Message);
        }
        finally
        {
            callback?.Invoke();
        }
        return res;
    }

    public static void HandleResult<T>(Result<List<T>> result,ref List<T>? list, PageError pageError)
    {
        if (result.IsSuccess)
        {
            if(list is null)
                list = result.Value;
            else
                list.AddRange(result.Value);
        }
        else
        {
            pageError.SetOnFailure(result);
        }
    }
}