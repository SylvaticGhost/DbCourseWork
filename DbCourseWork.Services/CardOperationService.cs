using Ardalis.Result;
using Core.Models;
using DbCourseWork.Repositories;
using ResultExtensions = DbCourseWork.Utils.ResultExtensions;

namespace DbCourseWork.Services;

public class CardOperationService(ICardOperationRepository cardOperationRepository) : ICardOperationService
{
    public Task<Result<IEnumerable<RideCardOperation>>> GetRides(long card) => 
        ResultExtensions.InErrorHandler(() => cardOperationRepository.GetRidesForCard(card));
}