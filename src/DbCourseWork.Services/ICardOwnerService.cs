using Ardalis.Result;
using Core.Models;

namespace DbCourseWork.Services;

public interface ICardOwnerService : ISearchableService<CardOwner>
{
    public Task<Result<CardOwner>> Find(int id);
}