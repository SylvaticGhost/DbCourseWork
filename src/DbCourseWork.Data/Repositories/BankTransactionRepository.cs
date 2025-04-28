using Core.Models;
using Core.Models.Systems;
using Data.Abstracrtions;
using Data.Context;

namespace Data.Repositories;

public class BankTransactionRepository(DataContext dataContext) : Repository<BankTransaction>(dataContext),IBankTransactionRepository
{
    protected override SortingField DefaultSortingField => new("time");
    protected override string CollectionName => "bank_transactions";
}