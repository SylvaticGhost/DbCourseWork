using Core.Models;

namespace DbCourseWork.Repositories;

public interface IBankTransactionRepository : IRepositoryInstance
{
    public Task InsertRange(IEnumerable<BankTransaction> transactions);
}