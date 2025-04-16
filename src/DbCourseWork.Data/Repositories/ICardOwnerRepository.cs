using Core.Models;
using Data.Abstracrtions;

namespace Data.Repositories;

public interface ICardOwnerRepository : IRepository<CardOwner>
{
    public Task<CardOwner?> Find(int id);
}