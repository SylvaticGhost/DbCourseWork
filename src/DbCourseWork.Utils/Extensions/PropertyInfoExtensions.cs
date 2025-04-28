using System.Reflection;

namespace Utils.Extensions;

public static class PropertyInfoExtensions
{
    public static T GetValueFromAttribute<TAttribute, T>(this PropertyInfo propertyInfo, Func<TAttribute, T> func)
        where TAttribute : Attribute
    {
        var attribute = propertyInfo.GetCustomAttribute<TAttribute>();
        if (attribute == null)
            throw new InvalidOperationException(
                $"Property {propertyInfo.Name} does not have attribute {typeof(TAttribute).Name}.");

        return func(attribute);
    }
}