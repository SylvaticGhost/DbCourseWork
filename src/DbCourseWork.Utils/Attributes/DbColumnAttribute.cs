using JetBrains.Annotations;

namespace Utils.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DbColumnAttribute(string columnName) : Attribute
{
    public string ColumnName { get; } = columnName;
}