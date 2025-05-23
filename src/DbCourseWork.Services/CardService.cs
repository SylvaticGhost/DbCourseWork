using Ardalis.Result;
using Core.Models;
using Data.Repositories;
using Services.Abstractions;
using ResultExtensions = Utils.Extensions.ResultExtensions;

namespace Services;

public class CardService(ICardRepository cardRepository) : SearchableService<TravelCard>(cardRepository),ICardService
{
    public Task<Result<IEnumerable<TravelCard>>> GetCardForUser(int userId) =>
        ResultExtensions.InErrorHandler(() => cardRepository.GetCardForUser(userId));
    
    public Task<Result<TravelCard?>> Find(long code) =>
        ResultExtensions.InErrorHandler(() => cardRepository.Find(code));
}