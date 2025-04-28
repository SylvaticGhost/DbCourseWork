using Ardalis.Result;
using Utils;
using Utils.Extensions;

namespace WebUI.Utils;

public record PageError()
{
    private string? _value;
    
    public void Clear() => _value = null;
    
    public void Set(string value) => _value = value;
    
    public bool Has => !string.IsNullOrEmpty(_value);

    public void SetOnFailure(Result result)
    {
        if(!result.IsSuccess) 
            _value = result.JoinErrorMessage();
    }
    
    public void SetOnFailure<T>(Result<T> result)
    {
        if(!result.IsSuccess) 
            _value = result.JoinErrorMessage();
    }

    public override string ToString() => this;

    public static implicit operator string(PageError pageError) => pageError._value ?? string.Empty;
}
