using DbCourseWork.Data;
using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public class BankTransactionRepository(DataContext dataContext) : Repository<BankTransaction>(dataContext),IBankTransactionRepository
{
    protected override SortingField DefaultSortingField => new("time");
    protected override string CollectionName => "bank_transactions";
    protected override string[] Columns => BankTransaction.Columns;
}