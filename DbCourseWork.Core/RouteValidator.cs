using Core.Enums;
using Route = Core.Models.Route;

namespace DbCourseWork.Helpers;

public static class RouteValidator
{
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

    public static string? ValidateName(string? name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return "Назва маршруту не може бути пустою";
        
        if (name.Length > 70)
            return "Назва маршруту не може перевищувати 70 символів";
        
        return null;
    }
}