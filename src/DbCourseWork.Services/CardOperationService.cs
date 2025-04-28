using Ardalis.Result;
using Core.Models;
using Data.Repositories;
using ResultExtensions = Utils.Extensions.ResultExtensions;

namespace Services;

public class CardOperationService(ICardOperationRepository cardOperationRepository) : ICardOperationService
{
    public Task<Result<IEnumerable<RideCardOperation>>> GetRides(long card) => 
        ResultExtensions.InErrorHandler(() => cardOperationRepository.GetRidesForCard(card));
}