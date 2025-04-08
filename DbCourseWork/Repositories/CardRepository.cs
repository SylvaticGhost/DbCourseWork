using System.Text;
using Dapper;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Models.Enums;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public class CardRepository(DataContext dataContext) : ICardRepository
{
    public Task<IEnumerable<TravelCard>> Get(CardSearchParam parameters)
    {
        var sb = new StringBuilder().AppendLine("SELECT * FROM travel_cards");

        if (parameters.OrderFields?.Count > 0)
        {
            sb.AppendLine("ORDER BY");
            foreach (SortingField<CardOrderField> field in parameters.OrderFields)
            {
                sb.AppendLine($"{field.Field} {field.Order}");
                if (field != parameters.OrderFields.Last())
                    sb.AppendLine(",");
            }
        }

        var sql = sb.ToString();

        return dataContext.LoadData<TravelCard>(sql);
    }

    public Task<TravelCard?> Find(long code)
    {
        const string sql = "SELECT * FROM travel_cards WHERE code = @code";
        var parameters = new DynamicParameters();
        parameters.Add("code", code);
        return dataContext.LoadDataSingle<TravelCard?>(sql, parameters);
    }

    public Task<IEnumerable<TravelCard>> GetCardForUser(int userId)
    {
        const string sql = "SELECT * FROM travel_cards WHERE owner = @userId";
        var parameters = new DynamicParameters();
        parameters.Add("userId", userId);
        return dataContext.LoadData<TravelCard>(sql, parameters);
    }
}