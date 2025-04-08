using Ardalis.Result;
using DbCourseWork.Models;
using DbCourseWork.Repositories;

namespace DbCourseWork.Services;

public class CardOwnerService(ICardOwnerRepository cardOwnerRepository) : ICardOwnerService
{
    public Task<Result<IEnumerable<CardOwner>>> Search(SearchParameters parameters) =>
        cardOwnerRepository.Search(parameters.Page, parameters.PageSize, "")
            .ContinueWith(t => 
                t.IsFaulted ? Result<IEnumerable<CardOwner>>.Error(t.Exception?.Message ?? "Unknown error")
                    : Result<IEnumerable<CardOwner>>.Success(t.Result));

    public Task<Result<CardOwner>> Find(int id)
    {
        var user = cardOwnerRepository.Find(id);
        
        return user.ContinueWith(t => 
            t.IsFaulted ? Result<CardOwner>.Error(t.Exception?.Message ?? "Unknown error")
                : t.Result is null ? Result<CardOwner>.NotFound()
                : Result<CardOwner>.Success(t.Result));
    }
}