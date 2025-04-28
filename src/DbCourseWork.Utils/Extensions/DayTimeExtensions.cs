namespace Utils.Extensions;

public static class DayTimeExtensions
{
    public static string ToUkr(this DateOnly date) => date.ToString("dd.MM.yyyy");
}