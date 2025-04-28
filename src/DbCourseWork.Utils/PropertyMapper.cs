using System.Reflection;
using Utils.Attributes;

namespace Utils;

public static class PropertyMapper
{
    public static string[] GetFormFields(Type type) =>
        GetByAttribute<UkrFormFieldAttribute>(type)
            .GetAttributeParams<string, UkrFormFieldAttribute>(a => a.Name);

    public static string[] GetDbColumnNames(Type type) =>
        GetByAttribute<DbColumnAttribute>(type).GetAttributeParams<string, DbColumnAttribute>(a => a.ColumnName);

    private static IReadOnlyDictionary<string, string> Map(Type type, DictMapOptions options) => type.GetProperties()
        .Where(options.Filter).Select(options.PropertyToPairFunc).ToDictionary(options.KeyMapFunc, options.ValMapFunc);

    private record DictMapOptions
    {
        public required Func<Tuple<string, string>, string> KeyMapFunc { get; init; }
        public required Func<Tuple<string, string>, string> ValMapFunc { get; init; }
        public required Func<PropertyInfo, Tuple<string, string>> PropertyToPairFunc { get; init; }
        public required Func<PropertyInfo, bool> Filter { get; init; }

        public static DictMapOptions For<TAttribute>(Func<Tuple<string, string>, string> keyMapFunc,
            Func<Tuple<string, string>, string> valMapFunc, Func<TAttribute, string> valueFromAttributeRetriever)
            where TAttribute : Attribute => new()
        {
            KeyMapFunc = keyMapFunc,
            ValMapFunc = valMapFunc,
            PropertyToPairFunc = prop =>
                new Tuple<string, string>(prop.Name,
                    valueFromAttributeRetriever(prop.GetCustomAttribute<TAttribute>()!)),
            Filter = prop => Attribute.IsDefined(prop, typeof(TAttribute))
        };
    }

    public static IReadOnlyDictionary<string, string> MapPropertyToUkrName(Type type) =>
        Map(type, DictMapOptions.For<UkrFormFieldAttribute>(t => t.Item1, t => t.Item2, a => a.Name));

    public static IReadOnlyDictionary<string, string> MapUkrToPropertyName(Type type) =>
        Map(type, DictMapOptions.For<UkrFormFieldAttribute>(t => t.Item2, t => t.Item1, a => a.Name));

    public static Type GetTypeOfProperty(string field, Type type)
    {
        var property = type.GetProperty(field);
        if (property == null)
            throw new InvalidOperationException($"Type {type.Name} does not have a property {field}.");

        return property.PropertyType;
    }

    public static Type GetTypeOfProperty<T>(string field) => GetTypeOfProperty(field, typeof(T));

    public static PropertyInfo[] GetByAttribute(Type type, Type attributeType) => type.GetProperties()
        .Where(prop => Attribute.IsDefined(prop, attributeType)).ToArray();

    public static PropertyInfo[] GetByAttribute<TAttribute>(Type type) where TAttribute : Attribute =>
        GetByAttribute(type, typeof(TAttribute));

    public static Func<object, object?> GetValueGetter(Type type, string propertyName)
    {
        var property = type.GetProperty(propertyName);
        if (property == null)
            throw new InvalidOperationException($"Type {type.Name} does not have a property {propertyName}.");

        return GetValueGetter(type, property);
    }

    private static Func<object, object?> GetValueGetter(Type type, PropertyInfo property)
    {
        var getter = property.GetGetMethod();
        if (getter == null)
            throw new InvalidOperationException($"Property {property.Name} does not have a getter.");

        return obj => getter.Invoke(obj, null);
    }
    
    private static T[] GetAttributeParams<T, TAttribute>(this PropertyInfo[] props, Func<TAttribute, T> selector)
        where TAttribute : Attribute => props.Select(prop => prop.GetCustomAttribute<TAttribute>()!)
        .Select(selector).ToArray();
}