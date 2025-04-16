using System.Text;
using Utils;

namespace Core.Models.Reports;

public record PerDayReport : PerTimeReport<DayOfWeek, AllDailyUsage>
{
    private PerDayReport(Dictionary<DayOfWeek, AllDailyUsage?> dictionary) : base(dictionary) {}
    
    public static PerDayReport Create(IEnumerable<DayRowData> data)
    {
        IEnumerable<DayRowData> dayRowDatas = data as DayRowData[] ?? data.ToArray();
        var dict = new Dictionary<DayOfWeek, AllDailyUsage?>();

        IEnumerable<DayOfWeek> days = Enum.GetValues<DayOfWeek>().OrderByUkrDayOrder();
        foreach (var day in days)
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
        foreach (var (day, usage) in Values)
        {
            sb.AppendLine($"{day}: {usage}");
        }
        return sb.ToString();
    }
    
    // public Dictionary<string, long[]> TravelCardVsBankCard() {}
}