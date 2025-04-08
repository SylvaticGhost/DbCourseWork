using Ardalis.Result;
using DbCourseWork.Models;
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

        if (dataDto.HasCards)
            await unit.Of<CardOperationRepository>().InsertRange(dataDto.CardOperations!);
        
        return Result.Success();
    });
}