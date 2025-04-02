using DbCourseWork.Models;

namespace DbCourseWork.Repositories;

public interface ICardOwnerRepository
{
    public Task<IEnumerable<CardOwner>> Search(int page = 1, int pageSize = 30, string search = "");
}