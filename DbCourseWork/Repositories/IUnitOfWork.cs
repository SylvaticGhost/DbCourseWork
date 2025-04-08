namespace DbCourseWork.Repositories;

public interface IUnitOfWork
{
    public TRepository Of<TRepository>() where TRepository : IRepository;

    public Task<TResult> InTransaction<TResult>(Func<IUnitOfWork, Task<TResult>> func);
}