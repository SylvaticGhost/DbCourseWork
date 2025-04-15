namespace Core.Interfaces;

public interface IFormTableEntity
{
    public string[] RowDisplayValues { get; }
    
    public string? UrlOnPage { get; }
}