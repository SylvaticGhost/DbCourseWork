using Ardalis.Result;
using Core.Models.Reports;

namespace Services;

public interface IRouteReportService
{
    public Task<Result<RouteReport>> GetReport(RouteReportParam param);
}