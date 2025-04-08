using Ardalis.Result;
using DbCourseWork.Models;

namespace DbCourseWork.Services;

public interface IDataImportService
{
    public Task<Result> ImportData(ImportDataDto dataDto);
}