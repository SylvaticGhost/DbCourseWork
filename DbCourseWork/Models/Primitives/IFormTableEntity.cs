namespace DbCourseWork.Models.Primitives;

public interface IFormTableEntity
{
    public string[] RowDisplayValues { get; }
    
    public string? UrlOnPage { get; }
}