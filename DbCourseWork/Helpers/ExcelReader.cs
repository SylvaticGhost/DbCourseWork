using Ardalis.Result;
using ClosedXML.Excel;
using DbCourseWork.Models;
using DbCourseWork.Utils;
using Route = DbCourseWork.Models.Route;

namespace DbCourseWork.Helpers;

public static class ExcelReader
{
    public static Result<List<Ride>> ReadRides(Stream stream, int worksheetPosition = 1) =>
        Read(stream, RowConverters.ToRide, worksheetPosition);
    
    public static Result<List<BankTransaction>> ReadBankTransactions(Stream stream, int worksheetPosition = 1) =>
        Read(stream, RowConverters.ToBankTransaction, worksheetPosition);
    
    public static Result<List<CardOperation>> ReadCardOperations(Stream stream, int worksheetPosition = 1) =>   
        Read(stream, RowConverters.ToCardOperation, worksheetPosition);

    private static Result<List<T>> Read<T>(Stream stream, Func<IXLRow, int, Result<T>> convertFunc, int worksheetPosition = 1, bool skipFirst = true)
    {
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(worksheetPosition);
        IEnumerable<IXLRow> rows = worksheet.RowsUsed().Skip(skipFirst ? 1 : 0);
        List<T> items = [];

        foreach (var row in rows)
        {
            Result<T> result = convertFunc(row, 0);
            if (!result.IsSuccess)
                return Result<List<T>>.Error(result.JoinErrorMessage());

            items.Add(result.Value);
        }

        return Result<List<T>>.Success(items);
    }

    private static class RowConverters
    {
        private static string RowMessage(int rowIndex) =>
            rowIndex > 0 ? $" at row {rowIndex}" : string.Empty;

        private static string ErrorMessage(string field, int rowIndex) =>
            $"Invalid {field} value{RowMessage(rowIndex)}";

        public static Result<Ride> ToRide(IXLRow row, int rowIndex = 0)
        {
            if (!Guid.TryParse(row.Cell(1).Value.ToString(), out var id))
                return Result<Ride>.Error(ErrorMessage("ідентифікатор поїздки", rowIndex));

            if (!int.TryParse(row.Cell(2).Value.ToString(), out int vehicle))
                return Result<Ride>.Error(ErrorMessage("номер транспортного засобу", rowIndex));

            var routeNumber = RouteNumber.Parse(row.Cell(3).Value.ToString());
            
            return Result<Ride>.Success(new Ride(id, vehicle, routeNumber));
        }
        
        public static Result<BankTransaction> ToBankTransaction(IXLRow row, int rowIndex = 0) 
        {
            if (!decimal.TryParse(row.Cell(1).Value.ToString(), out var bin))
                return Result<BankTransaction>.Error(ErrorMessage("МФО", rowIndex));

            if (!decimal.TryParse(row.Cell(2).Value.ToString(), out var account))
                return Result<BankTransaction>.Error(ErrorMessage("рахунку", rowIndex));

            if (!float.TryParse(row.Cell(3).Value.ToString(), out var amount))
                return Result<BankTransaction>.Error(ErrorMessage("сума транзакції", rowIndex));

            if (!DateTime.TryParse(row.Cell(4).Value.ToString(), out var time))
                return Result<BankTransaction>.Error(ErrorMessage("дата", rowIndex));

            if (!Guid.TryParse(row.Cell(5).Value.ToString(), out var rideId))
                return Result<BankTransaction>.Error(ErrorMessage("ідентифікатор поїздки", rowIndex));

            return Result<BankTransaction>.Success(new BankTransaction(bin, account, amount, time, rideId));
        }

        public static Result<CardOperation> ToCardOperation(IXLRow row, int rowIndex = 0)
        {
            if (!long.TryParse(row.Cell(1).Value.ToString(), out long card))
                return Result<CardOperation>.Error(ErrorMessage("номер картки", rowIndex));
            
            if (!Guid.TryParse(row.Cell(2).Value.ToString(), out var rideId))
                return Result<CardOperation>.Error(ErrorMessage("ідентифікатор поїздки", rowIndex));
            
            if (!DateTime.TryParse(row.Cell(3).Value.ToString(), out var date))
                return Result<CardOperation>.Error(ErrorMessage("дата", rowIndex));
            
            if (!int.TryParse(row.Cell(4).Value.ToString(), out var change))
                return Result<CardOperation>.Error(ErrorMessage("зміна кількості поїздок", rowIndex));
            
            return Result<CardOperation>.Success(new CardOperation(card, rideId, date, change));
        }
    }
}