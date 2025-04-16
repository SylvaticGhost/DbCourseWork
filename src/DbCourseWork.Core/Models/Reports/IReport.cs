namespace Core.Models.Reports;

public interface IReport<out TUsage> where TUsage : ITimeUsage
{
    public Dictionary<string, long[]> ToDictionary(Func<TUsage, long>[] dimensions);
}