using Core.Enums;
using Utils;

namespace Core.Models.Systems;

public struct RouteNumber
{
    private string _value;

    public RouteNumber(string value)
    {
        var validationError = ValidateNumber(value);
        if (validationError != null)
            throw new ArgumentException(validationError, nameof(value));
        _value = value;
    }

    public static bool TryParse(string str, out RouteNumber routeNumber)
    {
        Console.WriteLine($"Converting '{str}' to RouteNumber");
        var validationError = ValidateNumber(str);
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
    
    public static string? ValidateNumber(string? number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return "Номер маршрута не може бути пустим";

        if (number.Length is < 2 or > 6)
            return "Номер маршрута повинен містити від 2 до 6 символів";

        string prefix = Route.GetPrefix(number);

        if (!VehicleMapper.IsPrefixValid(prefix))
            return "Префікс маршруту не коректний";

        return null;
    }
    
    public const string RegexPattern = @"^[\u0410-\u044F]{1,2}\d{0,4}$";
}