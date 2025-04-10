using DbCourseWork.Models.Primitives;

namespace DbCourseWork.Repositories;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : IDbEntity
{
    
}