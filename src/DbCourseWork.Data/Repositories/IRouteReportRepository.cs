using Core.Models.Reports;

namespace Data.Repositories;

public interface IRouteReportRepository
{
    public Task<IEnumerable<PassengersForSource>> GetPassengers(RouteReportParam param);

    public Task<IEnumerable<DayRowData>> GetDailyData(RouteReportParam param);

    public Task<IEnumerable<HourRowData>> GetHourlyData(RouteReportParam param);
}