using System.Text;

namespace Core.Models.Reports;

public record PerHourReport : PerTimeReport<int, AllHourlyUsage>
{
    private const byte HoursInDay = 24;

    private PerHourReport(Dictionary<int, AllHourlyUsage?> dictionary) : base(dictionary) {}

    public static PerHourReport Create(IEnumerable<HourRowData> data)
    {
        IEnumerable<HourRowData> hourRowDatas = data as HourRowData[] ?? data.ToArray();
        var dict = new Dictionary<int, AllHourlyUsage?>();
        
        for (var i = 0; i < HoursInDay; i++)
        {
            HourRowData[] row = hourRowDatas.Where(x => x.Hour == i).ToArray();

            AllHourlyUsage? value;
            
            value = row.Length == 0 ? null : AllHourlyUsage.Create(row);
            dict.Add(i, value);
        }
        
        return new PerHourReport(dict);
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var (hour, usage) in Values)
        {
            sb.AppendLine($"{hour}: {usage}");
        }

        return sb.ToString();
    }
    
}