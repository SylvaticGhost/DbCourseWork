using Data.Abstracrtions;
using Data.Context;

namespace Data.UnitOfWork;

public class UnitOfWork(DataContext dataContext) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public TRepository Of<TRepository>() where TRepository : IRepositoryInstance
    {
        if (_repositories.TryGetValue(typeof(TRepository), out object? repository))
            return (TRepository)repository;

        var newRepository = Activator.CreateInstance(typeof(TRepository), dataContext) ??
                            throw new InvalidOperationException(
                                $"Cannot create repository of type {typeof(TRepository)}");
        
        var repo = (TRepository)newRepository;
        
        if (repo is null)
            throw new InvalidOperationException(
                $"Cannot create repository of type {typeof(TRepository)}");
        
        _repositories.Add(typeof(TRepository), repo);
        return repo;
    }

    public async Task<TResult> InTransaction<TResult>(Func<IUnitOfWork, Task<TResult>> func)
    {
        var transaction = await dataContext.BeginTransaction();
        try
        {
            var result = await func(this);
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}