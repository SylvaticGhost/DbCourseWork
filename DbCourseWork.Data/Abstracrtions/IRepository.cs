using Core.Interfaces;

namespace DbCourseWork.Repositories;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : IDbEntity
{
    public Task Insert(TEntity entity);
}