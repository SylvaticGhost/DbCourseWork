namespace Data.Utils;

internal static class SqlTextHelper
{
    public static string FormatValue(object? value) => value switch
    {
        null => "NULL",
        string or char => $"'{value.ToString()?.Replace("'", "''")}'",
        DateTime dateTime => $"'{dateTime:yyyy-MM-dd HH:mm:ss}'",
        bool boolVal => boolVal ? "true" : "false",
        _ => value.ToString() ?? "NULL"
    };
}