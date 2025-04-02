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
}