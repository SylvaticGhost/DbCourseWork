using Ardalis.Result;
using Core.Models.DTOs;
using DbCourseWork.Repositories;

namespace DbCourseWork.Services;

public class DataImportService(IUnitOfWork unitOfWork) : IDataImportService
{
    public Task<Result> ImportData(ImportDataDto dataDto) => unitOfWork.InTransaction<Result>(async unit =>
    {
        if(dataDto.HasRides)
            await unit.Of<RideRepository>().InsertRange(dataDto.Rides!);
            
        if(dataDto.HasBankTransactions)
            await unit.Of<BankTransactionRepository>().InsertRange(dataDto.BankTransactions!);

        if (dataDto.HasCardOwners)
            await unit.Of<CardOwnerRepository>().InsertRange(dataDto.CardOwners!);
        
        if (dataDto.HasTravelCards) 
            await unit.Of<CardRepository>().InsertRange(dataDto.TravelCards!);
        
        if (dataDto.HasCardOperations)
            await unit.Of<CardOperationRepository>().InsertRange(dataDto.CardOperations!);
        
        return Result.Success();
    });
}