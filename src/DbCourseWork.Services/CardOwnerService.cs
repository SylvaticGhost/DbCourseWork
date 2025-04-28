using Ardalis.Result;
using Core.Models;
using Data.Repositories;
using Services.Abstractions;

namespace Services;

public class CardOwnerService(ICardOwnerRepository cardOwnerRepository)
    : SearchableService<CardOwner>(cardOwnerRepository), ICardOwnerService
{
    public Task<Result<CardOwner>> Find(int id)
    {
        var user = cardOwnerRepository.Find(id);

        return user.ContinueWith(t => t.IsFaulted ? Result<CardOwner>.Error(t.Exception?.Message ?? "Unknown error") :
            t.Result is null ? Result<CardOwner>.NotFound() : Result<CardOwner>.Success(t.Result));
    }
}