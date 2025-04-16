using Core.Models;
using Data.Abstracrtions;

namespace Data.Repositories;

public interface IBankTransactionRepository : IRepositoryInstance
{
    public Task InsertRange(IEnumerable<BankTransaction> transactions);
}