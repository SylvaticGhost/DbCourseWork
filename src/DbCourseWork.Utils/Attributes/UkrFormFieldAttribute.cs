namespace Utils.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class UkrFormFieldAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}