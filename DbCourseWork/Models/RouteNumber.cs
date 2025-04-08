using DbCourseWork.Helpers;

namespace DbCourseWork.Models;

public struct RouteNumber
{
    private string _value;

    public RouteNumber(string value)
    {
        var validationError = RouteValidator.ValidateNumber(value);
        if (validationError != null)
            throw new ArgumentException(validationError, nameof(value));
        _value = value;
    }

    public static bool TryParse(string str, out RouteNumber routeNumber)
    {
        Console.WriteLine($"Converting '{str}' to RouteNumber");
        var validationError = RouteValidator.ValidateNumber(str);
        if (validationError != null)
        {
            routeNumber = default;
            return false;
        }
        
        routeNumber = new RouteNumber(str);
        return true;
    }

    public static RouteNumber Parse(string str)
    {
        string value = LocalizationHelper.ToCyrillicLetters(str);
        return new RouteNumber(value);
    }

    public static implicit operator string(RouteNumber routeNumber) => routeNumber._value;
    
    public static implicit operator RouteNumber(string str) => new RouteNumber(str);
    
    public override string ToString() => _value;
}