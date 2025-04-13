using System.Text;

namespace DbCourseWork.Models.Reports;

public class PerDayReport
{
    private readonly IReadOnlyDictionary<DayOfWeek, AllDailyUsage?> _values;
    
    private PerDayReport(Dictionary<DayOfWeek, AllDailyUsage?> dictionary) => _values = dictionary;
    
    public static PerDayReport Create(IEnumerable<DayRowData> data)
    {
        IEnumerable<DayRowData> dayRowDatas = data as DayRowData[] ?? data.ToArray();
        var dict = new Dictionary<DayOfWeek, AllDailyUsage?>();
        
        foreach (var day in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
        {
            DayRowData[] row = dayRowDatas.Where(x => x.Day == day).ToArray();

            AllDailyUsage? value;
            
            value = row.Length == 0 ? null : AllDailyUsage.Create(row);
            dict.Add(day, value);
        }
        
        return new PerDayReport(dict);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var (day, usage) in _values)
        {
            sb.AppendLine($"{day}: {usage}");
        }
        return sb.ToString();
    }
}