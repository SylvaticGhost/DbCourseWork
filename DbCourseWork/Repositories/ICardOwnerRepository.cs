using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface ICardOwnerRepository : IRepository<CardOwner>
{
    public Task<IEnumerable<CardOwner>> Search(int page = 1, int pageSize = 30, string search = "");

    public Task<CardOwner?> Find(int id);
}