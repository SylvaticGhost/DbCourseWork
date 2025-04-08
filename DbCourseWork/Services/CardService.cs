using Ardalis.Result;
using DbCourseWork.Models;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;

namespace DbCourseWork.Services;

public class CardService(ICardRepository cardRepository) : ICardService
{
    public Task<Result<IEnumerable<TravelCard>>> Search(CardSearchParam param) => 
        ResultExtensions.InErrorHandler(() => cardRepository.Get(param));
    
    public Task<Result<IEnumerable<TravelCard>>> GetCardForUser(int userId) =>
        ResultExtensions.InErrorHandler(() => cardRepository.GetCardForUser(userId));
    
    public Task<Result<TravelCard?>> Find(long code) =>
        ResultExtensions.InErrorHandler(() => cardRepository.Find(code));
}