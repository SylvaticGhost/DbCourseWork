using Core.Models;

namespace DbCourseWork.Repositories;

public interface ICardOwnerRepository : IRepository<CardOwner>
{
    public Task<CardOwner?> Find(int id);
}