using Utils;
using Utils.Extensions;

namespace Core.Models.Reports;

public abstract record PerTimeReport<TKey, TUsage> : IReport<TUsage> where TKey : notnull where TUsage : ITimeUsage
{
    protected readonly IReadOnlyDictionary<TKey, TUsage?> Values;

    protected PerTimeReport(Dictionary<TKey, TUsage?> dictionary) => Values = dictionary;

    public Dictionary<string, long[]> ToDictionary(Func<TUsage, long>[] dimensions)
    {
        var result = new Dictionary<string, long[]>();
        foreach (var (key, usage) in Values)
        {
            if (usage is null)
            {
                result.Add(FormatKey(key), []);
                continue;
            }

            List<long> values = [];
            values.AddRange(dimensions.Select(dimension => dimension(usage)));
            result.Add(usage.KeyToUkrString(), values.ToArray());
        }

        return result;
    }

    private static string FormatKey(TKey key)
    {
        if (typeof(TKey) != typeof(DayOfWeek))
            return key.ToString()!;
        
        var dayOfWeek = (DayOfWeek)(object)key;
        return dayOfWeek.ToUkrString();
    }
}