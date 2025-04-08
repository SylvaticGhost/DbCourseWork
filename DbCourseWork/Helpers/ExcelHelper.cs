namespace DbCourseWork.Helpers;

public class ExcelHelper
{
    public static readonly string[] FileFormats = [".xlsx", ".xls"];
    
    public static string FileFormatsAsString => string.Join(", ", FileFormats);
}