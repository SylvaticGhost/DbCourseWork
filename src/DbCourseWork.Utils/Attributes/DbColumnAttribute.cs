using JetBrains.Annotations;

namespace Utils.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DbColumnAttribute([UsedImplicitly]string ColumnName) : Attribute;