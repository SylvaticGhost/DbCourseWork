namespace Utils.Attributes;

public static class EnumExtensions
{
    public static object GetDefault(Type type)
    {
        if (!type.IsEnum)
            throw new ArgumentException($"Type {type.Name} is not an enum.");
        
        var values = Enum.GetValues(type);
        return values.Length > 0 ? values.GetValue(0)! :
            Enum.ToObject(type, 0);
    }
}