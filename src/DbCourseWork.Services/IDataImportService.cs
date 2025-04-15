using Ardalis.Result;
using Core.Models.DTOs;

namespace DbCourseWork.Services;

public interface IDataImportService
{
    public Task<Result> ImportData(ImportDataDto dataDto);
}