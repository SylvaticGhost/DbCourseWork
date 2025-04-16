namespace Utils;

public static class DayOfWeekExtensions
{
    private static readonly IReadOnlyDictionary<DayOfWeek, string> DayOfWeekUrk = new Dictionary<DayOfWeek, string>
    {
        { DayOfWeek.Monday, "Понеділок" },
        { DayOfWeek.Tuesday, "Вівторок" },
        { DayOfWeek.Wednesday, "Середа" },
        { DayOfWeek.Thursday, "Четвер" },
        { DayOfWeek.Friday, "П'ятниця" },
        { DayOfWeek.Saturday, "Субота" },
        { DayOfWeek.Sunday, "Неділя" }
    };

    public static string ToUkrString(this DayOfWeek dayOfWeek)
    {
        if (DayOfWeekUrk.TryGetValue(dayOfWeek, out var result))
            return result;

        throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, "Invalid day of week");
    }

    public static string TranslateToUkrIfDayOfWeek(string dayOfWeek) =>
        Enum.TryParse(typeof(DayOfWeek), dayOfWeek, true, out var result)
            ? ((DayOfWeek)result).ToUkrString()
            : dayOfWeek;
    
    public static IEnumerable<DayOfWeek> OrderByUkrDayOrder(this DayOfWeek[] days) =>
        days.OrderBy(d => (int)d == 0 ? 7 : (int)d);
}