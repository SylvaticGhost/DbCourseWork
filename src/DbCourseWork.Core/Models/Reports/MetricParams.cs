namespace Core.Models.Reports;

public record MetricParams(Dictionary<string, long[]> Data, string Title, string[] Legends);