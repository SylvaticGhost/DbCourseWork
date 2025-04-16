using Ardalis.Result;
using Core.Models.DTOs;

namespace Services;

public interface IDataImportService
{
    public Task<Result> ImportData(ImportDataDto dataDto);
}