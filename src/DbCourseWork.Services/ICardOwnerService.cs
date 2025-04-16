using Ardalis.Result;
using Core.Models;

namespace Services;

public interface ICardOwnerService : ISearchableService<CardOwner>
{
    public Task<Result<CardOwner>> Find(int id);
}