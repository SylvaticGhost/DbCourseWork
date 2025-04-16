using Ardalis.Result;
using Core.Models.Reports;
using Data.Repositories;
using ResultExtensions = Utils.ResultExtensions;

namespace Services;

public class RouteReportService(IRouteReportRepository routeReportRepository) : IRouteReportService
{
    public Task<Result<RouteReport>> GetReport(RouteReportParam param) =>
        ResultExtensions.InErrorHandler(async () =>
        {
            IEnumerable<PassengersForSource> passengers = await routeReportRepository.GetPassengers(param);
            IEnumerable<DayRowData> dailyData = await routeReportRepository.GetDailyData(param);
            IEnumerable<HourRowData> hourlyData = await routeReportRepository.GetHourlyData(param);

            var perDayReport = PerDayReport.Create(dailyData);
            var perHourReport = PerHourReport.Create(hourlyData);

           return RouteReport.Create(param, passengers, perDayReport, perHourReport);
        }, throwInDebug: true);
}