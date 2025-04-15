namespace Core.Models.Reports;

public record HourRowData
{
    public int Hour { get; init; }
    
    public long Passengers { get; init; }
    
    public string Source { get; init; }

    public HourRowData(decimal hour, long passengers, string source)
    {
        Hour = (int)hour;
        Passengers = passengers;
        Source = source;
    }
}