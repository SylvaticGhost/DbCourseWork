using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JetBrains.Annotations;

namespace Utils.Attributes;

[AttributeUsage(AttributeTargets.Constructor)]
public sealed class DbConstructor : Attribute;

[UsedImplicitly]
internal static class ConstructorUsageEnsurer
{
    // This method exists solely to make the IDE recognize the constructor as used
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static T CreateFromDbConstructor<T>(params object[] parameters) where T : class
    {
        var type = typeof(T);
        var constructor = type.GetConstructors()
            .FirstOrDefault(c => c.GetCustomAttribute<DbConstructor>() != null);

        if (constructor == null)
            throw new InvalidOperationException($"No constructor with DbConstructor found for {type.Name}");

        return (T)constructor.Invoke(parameters);
    }
}