using Data.Abstracrtions;

namespace Data.UnitOfWork;

public interface IUnitOfWork
{
    public TRepository Of<TRepository>() where TRepository : IRepositoryInstance;

    public Task<TResult> InTransaction<TResult>(Func<IUnitOfWork, Task<TResult>> func);
}