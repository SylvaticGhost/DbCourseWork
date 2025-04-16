namespace Core.Models.Reports;

public class MetricBuilder<TUsage, TReport>(TReport report) where TUsage : ITimeUsage where TReport : IReport<TUsage>
{
    private readonly List<Func<TUsage, long>> _dimensions = [];
    
    private string? _title;

    private readonly List<string> _legends = [];

    private TReport _report = report;

    public MetricBuilder<TUsage, TReport> AddDimension(Func<TUsage, long> dimension, string legend)
    {
        _dimensions.Add(dimension);
        _legends.Add(legend);
        return this;
    }
    
    public MetricBuilder<TUsage, TReport> AddTitle(string title)
    {
        _title = title;
        return this;
    }

    public MetricParams Build()
    {
        Dictionary<string, long[]> data = _report.ToDictionary(_dimensions.ToArray());
        _title ??= _report.GetType().Name;
        return new MetricParams(data, _title, _legends.ToArray());
    }
}