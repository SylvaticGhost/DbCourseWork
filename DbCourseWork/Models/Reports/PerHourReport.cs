namespace DbCourseWork.Models.Reports;

public class PerHourReport
{
    private const byte HoursInDay = 24;
    private readonly IReadOnlyDictionary<int, AllHourlyUsage?> _values;

    private PerHourReport(Dictionary<int, AllHourlyUsage?> dictionary) => _values = dictionary;

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
    
}