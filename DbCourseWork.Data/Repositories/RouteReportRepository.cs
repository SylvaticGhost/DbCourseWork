using System.Data;
using Core.Models.Reports;
using Dapper;
using DbCourseWork.Data;

namespace DbCourseWork.Repositories;

public class RouteReportRepository(DataContext dataContext) : IRouteReportRepository
{
    public Task<IEnumerable<PassengersForSource>> GetPassengers(RouteReportParam param)
    {
        //language=sql
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
                               ABS(SUM(bt.amount))::decimal as amount, 
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
        //language=sql
        const string sql = """
                           WITH day_counts AS (
                               SELECT
                                   TO_CHAR(d, 'Day') AS day,
                                   COUNT(*) AS occurrences
                               FROM generate_series(
                                            @startDate,
                                            @endDate,
                                            INTERVAL '1 day'
                                    ) d
                               GROUP BY TO_CHAR(d, 'Day')
                           ),
                           
                                travel_card_stats AS (
                                    SELECT
                                        TO_CHAR(co.date, 'Day') AS day,
                                        COUNT(*) AS total_passengers,
                                        COUNT(DISTINCT co.card) AS uniq_cards
                                    FROM rides r
                                             LEFT JOIN card_operation co ON r.id = co.ride
                                    WHERE co.date BETWEEN @startDate AND @endDate
                                      AND r.route = @route
                                    GROUP BY TO_CHAR(co.date, 'Day')
                                ),
                           
                                bank_card_stats AS (
                                    SELECT
                                        TO_CHAR(bt.time, 'Day') AS day,
                                        COUNT(*) AS total_passengers,
                                        COUNT(DISTINCT bt.account) AS uniq_cards
                                    FROM rides r
                                             LEFT JOIN bank_transactions bt ON r.id = bt.ride
                                    WHERE bt.time BETWEEN @startDate AND @endDate
                                      AND r.route = @route
                                    GROUP BY TO_CHAR(bt.time, 'Day')
                                )
                           
                           SELECT
                               ROUND(tc.total_passengers::numeric / dc.occurrences)::bigint AS passengers,
                               ROUND(tc.uniq_cards::numeric / dc.occurrences)::bigint AS uniq,
                               tc.day,
                               'TravelCard' AS source
                           FROM travel_card_stats tc
                                    JOIN day_counts dc ON tc.day = dc.day
                           
                           UNION
                           
                           SELECT
                               ROUND(bc.total_passengers::numeric / dc.occurrences)::bigint AS passengers,
                               ROUND(bc.uniq_cards::numeric / dc.occurrences)::bigint AS uniq,
                               bc.day,
                               'BankCard' AS source
                           FROM bank_card_stats bc
                                    JOIN day_counts dc ON bc.day = dc.day;
                           """;
        
        return dataContext.LoadData<DayRowData>(sql, GetReportParameters(param));
    }

    public Task<IEnumerable<HourRowData>> GetHourlyData(RouteReportParam param)
    {
        //language=sql
        const string sql = """
                           WITH date_range AS (
                               SELECT
                                   DATE_TRUNC('day', dd) AS day
                               FROM generate_series(
                                            @startTime::timestamp,
                                            @endTime::timestamp,
                                            INTERVAL '1 day'
                                    ) dd
                           ),
                                day_count AS (
                                    SELECT COUNT(*) AS total_days FROM date_range
                                ),
                           
                                travel_card_hourly AS (
                                    SELECT
                                        EXTRACT(HOUR FROM co.date) AS hour,
                                        COUNT(*) AS total_passengers
                                    FROM rides r
                                             LEFT JOIN card_operation co ON r.id = co.ride
                                    WHERE
                                        co.date BETWEEN @startTime AND @endTime
                                      AND r.route = @route
                                    GROUP BY EXTRACT(HOUR FROM co.date)
                                ),
                           
                                bank_card_hourly AS (
                                    SELECT
                                        EXTRACT(HOUR FROM bt.time) AS hour,
                                        COUNT(*) AS total_passengers
                                    FROM rides r
                                             LEFT JOIN bank_transactions bt ON r.id = bt.ride
                                    WHERE
                                        bt.time BETWEEN @startTime AND @endTime
                                      AND r.route = @route
                                    GROUP BY EXTRACT(HOUR FROM bt.time)
                                )
                           
                           SELECT
                               tch.hour,
                               CEILING(tch.total_passengers::float / dc.total_days::float)::bigint AS passengers,
                               'TravelCard' AS source
                           FROM travel_card_hourly tch, day_count dc
                           
                           UNION
                           
                           SELECT
                               bch.hour,
                               CEILING(bch.total_passengers::float / dc.total_days::float)::bigint AS passengers,
                               'BankCard' AS source
                           FROM bank_card_hourly bch, day_count dc
                           ORDER BY hour, source;
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