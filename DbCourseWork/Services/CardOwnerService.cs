using Ardalis.Result;
using DbCourseWork.Models;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;

namespace DbCourseWork.Services;

public class CardOwnerService(ICardOwnerRepository cardOwnerRepository) : ICardOwnerService
{
    public Task<Result<IEnumerable<CardOwner>>> Search(SearchParameters parameters) =>
        ResultExtensions.InErrorHandler(cardOwnerRepository.Get(parameters));

    public Task<Result<CardOwner>> Find(int id)
    {
        var user = cardOwnerRepository.Find(id);
        
        return user.ContinueWith(t => 
            t.IsFaulted ? Result<CardOwner>.Error(t.Exception?.Message ?? "Unknown error")
                : t.Result is null ? Result<CardOwner>.NotFound()
                : Result<CardOwner>.Success(t.Result));
    }
}