using System.Text;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public class CardOperationRepository(DataContext dataContext) : ICardOperationRepository
{
    public Task InsertRange(IEnumerable<CardOperation> operations) 
    {
        var sb = new StringBuilder().AppendLine("INSERT INTO card_operation (card, date, change, ride) VALUES ");
        foreach (var operation in operations)
            sb.AppendLine($"({operation.Card}, '{operation.Date}', {operation.Change}, '{operation.Ride}')");

        var sql = sb.EndSql().ToString();
        return dataContext.ExecuteSql(sql);
    }
    
    public Task<IEnumerable<CardOperation>> GetForCard(long card)
    {
        const string sql = "SELECT * FROM card_operation WHERE card = @Card";
        var parameters = DynamicParametersExtensions.WithSingleParameter("Card", card);
        return dataContext.LoadData<CardOperation>(sql, parameters);
    }

    public Task<IEnumerable<RideCardOperation>> GetRidesForCard(long card)
    {
        const string sql = """
                           SELECT o.card, o.date, o.change, r.id, r.vehicle, r.route FROM card_operation o
                                    JOIN public.rides r on o.ride = r.id
                                    WHERE o.card = @Card
                           """;
        
        var parameters = DynamicParametersExtensions.WithSingleParameter("Card", card);
        return dataContext.LoadData<RideCardOperation>(sql, parameters);
    }
}