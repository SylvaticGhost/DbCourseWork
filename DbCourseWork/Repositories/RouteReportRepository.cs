using System.Data;
using Dapper;
using DbCourseWork.Data;
using DbCourseWork.Models.Reports;

namespace DbCourseWork.Repositories;

public class RouteReportRepository(DataContext dataContext) : IRouteReportRepository
{
    public Task<IEnumerable<PassengersForSource>> GetPassengers(RouteReportParam param)
    {
        const string sql = """
                           SELECT
                               COUNT(*) as passengers,
                               COUNT(DISTINCT co.card) as uniq,
                               0::decimal as amount,
                               'TravelCard' as Source
                           FROM rides r
                                    LEFT JOIN card_operation co ON r.id = co.ride
                           WHERE
                               co.date BETWEEN @startDate AND @endDate
                             AND
                               r.route=@route
                           GROUP BY r.route
                           UNION
                           SELECT
                               COUNT(*) as passengers,
                               COUNT(DISTINCT bt.account) as uniq,
                               SUM(bt.amount)::decimal as amount,
                               'BankCard' as Source
                           FROM rides r
                                    LEFT JOIN bank_transactions bt ON r.id = bt.ride
                           WHERE
                               bt.time BETWEEN @startDate AND @endDate
                             AND
                               r.route=@route
                           GROUP BY r.route;
                           """;
        
        return dataContext.LoadData<PassengersForSource>(sql, GetReportParameters(param));
    }

    public Task<IEnumerable<DayRowData>> GetDailyData(RouteReportParam param)
    {
        const string sql = """
                           SELECT
                               COUNT(*) as passengers,
                               COUNT(DISTINCT co.card) as uniq,
                               to_char(co.date, 'Day') as day,
                               'TravelCard' as Source
                           FROM rides r 
                           LEFT JOIN card_operation co ON r.id = co.ride
                           WHERE
                               co.date BETWEEN @startDate AND @endDate
                             AND
                               r.route=@route
                           GROUP BY to_char(co.date, 'Day')
                           UNION
                           SELECT
                               COUNT(*) as passengers,
                               COUNT(DISTINCT bt.account) as uniq,
                               to_char(bt.time, 'Day') as day,
                               'BankCard' as Source
                           FROM rides r
                                       LEFT JOIN bank_transactions bt ON r.id = bt.ride
                               WHERE
                                   bt.time BETWEEN @startDate AND @endDate
                               AND
                                   r.route=@route
                           GROUP BY to_char(bt.time, 'Day');
                           """;
        
        return dataContext.LoadData<DayRowData>(sql, GetReportParameters(param));
    }

    public Task<IEnumerable<HourRowData>> GetHourlyData(RouteReportParam param)
    {
        const string sql = """
                           SELECT
                               EXTRACT(HOUR FROM co.date) AS hour,
                               COUNT(*) as passengers,
                               'TravelCard' as Source
                           FROM rides r
                           LEFT JOIN card_operation co ON r.id = co.ride
                           WHERE
                               co.date BETWEEN @startTime AND @endTime
                               AND
                               r.route=@route
                           GROUP BY EXTRACT(HOUR FROM co.date)
                           UNION 
                           SELECT
                               EXTRACT(HOUR FROM bt.time) AS hour,
                               COUNT(*) as passengers,
                               'BankCard' as Source
                           FROM rides r
                           LEFT JOIN bank_transactions bt ON r.id = bt.ride
                           WHERE
                               bt.time BETWEEN @startTime AND @endTime
                               AND
                               r.route=@route
                           GROUP BY EXTRACT(HOUR FROM bt.time);
                           """;

        var startTime = DateTime.SpecifyKind(param.Start.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var endTime = DateTime.SpecifyKind(param.End.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Utc);
        
        var parameters = new DynamicParameters();
        parameters.Add("startTime", startTime, DbType.DateTime);
        parameters.Add("endTime", endTime, DbType.DateTime);
        parameters.Add("route", param.Number.ToString());
        return dataContext.LoadData<HourRowData>(sql, parameters);
    }
    
    private static DynamicParameters GetReportParameters(RouteReportParam param)
    {
        var parameters = new DynamicParameters();
        parameters.Add("route", param.Number.ToString());
        parameters.Add("startDate", param.Start, DbType.Date);
        parameters.Add("endDate", param.End, DbType.Date);
        return parameters;
    }
}