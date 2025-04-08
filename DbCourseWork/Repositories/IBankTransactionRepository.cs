using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface IBankTransactionRepository : IRepository
{
    public Task InsertRange(IEnumerable<BankTransaction> transactions);
}