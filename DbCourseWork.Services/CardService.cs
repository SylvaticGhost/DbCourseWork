using Ardalis.Result;
using Core.Models;
using Core.Models.Systems;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;

namespace DbCourseWork.Services;

public class CardService(ICardRepository cardRepository) : SearchableService<TravelCard>(cardRepository),ICardService
{
    public Task<Result<IEnumerable<TravelCard>>> GetCardForUser(int userId) =>
        ResultExtensions.InErrorHandler(() => cardRepository.GetCardForUser(userId));
    
    public Task<Result<TravelCard?>> Find(long code) =>
        ResultExtensions.InErrorHandler(() => cardRepository.Find(code));
}