using Ardalis.Result;
using Core.Models;
using Services.Abstractions;

namespace Services;

public interface ICardOwnerService : ISearchableService<CardOwner>
{
    public Task<Result<CardOwner>> Find(int id);
}