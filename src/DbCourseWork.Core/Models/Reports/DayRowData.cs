namespace Core.Models.Reports;

public record DayRowData
{
    public long Passengers { get; init; }

    public long UniquePassengers { get; init; }

    public DayOfWeek Day { get; init; }

    public string Source { get; init; }

    public DayRowData(long passengers, long uniq, string day, string source)
    {
        Passengers = passengers;
        UniquePassengers = uniq;
        Day = Enum.TryParse<DayOfWeek>(day, out var dayOfWeek)
            ? dayOfWeek
            : throw new ArgumentException("Invalid day of week");
        Source = source;
    }
}