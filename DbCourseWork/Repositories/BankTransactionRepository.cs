using System.Text;
using DbCourseWork.Data;
using DbCourseWork.Models;
using DbCourseWork.Utils;

namespace DbCourseWork.Repositories;

public class BankTransactionRepository(DataContext dataContext) : IBankTransactionRepository
{
    public Task InsertRange(IEnumerable<BankTransaction> transactions)
    {
        var sb = new StringBuilder().AppendLine("INSERT INTO bank_transactions (bin, account, amount, time, ride) VALUES ");
        foreach (var transaction in transactions)
            sb.AppendLine($"({transaction.Bin}, {transaction.Account}, {transaction.Amount}, '{transaction.Time}', '{transaction.Ride}')");
        
        var sql = sb.EndSql().ToString();
        return dataContext.ExecuteSql(sql);
    }
}