using System.Collections;

namespace Saorsa;


/// <summary>
/// Extension methods for Type class.
/// </summary>
public static class TypeUtilityExtensions
{
    /// <summary>
    /// Checks if a target type is a Nullable derivative.
    /// </summary>
    /// <param name="type">The source type object, required.</param>
    public static bool IsNullable(this Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }

    /// <summary>
    /// Gets the underlying type of a nullable type, if Nullable, the type itself otherwise.
    /// </summary>
    /// <param name="type">The source type object, required.</param>
    public static Type GetUnderlyingTypeIfNullable(this Type type)
    {
        return Nullable.GetUnderlyingType(type) ?? type;
    }

    /// <summary>
    /// Checks if a target type is a struct or not.
    /// </summary>
    /// <param name="type">The source type object, required.</param>
    public static bool IsStruct(this Type type)
    {
        return type is
        {
            IsValueType: true,
            IsEnum: false,
            IsPrimitive: false
        };
    }

    /// <summary>
    /// Checks if a type represents a generic enumeration.
    /// </summary>
    /// <param name="type">The source type object, required.</param>
    public static bool IsGenericEnumeration(this Type type)
    {
        return type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type);
    }

    /// <summary>
    /// Checks if a type represents a generic enumeration.
    /// </summary>
    /// <param name="type">The source type object, required.</param>
    /// <typeparam name="T">The type of objects in the enumeration to be validated.</typeparam>
    public static bool IsGenericEnumeration<T>(this Type type)
    {
        var genericTypeArgs = type.GetGenericArguments();
        return typeof(IEnumerable).IsAssignableFrom(type)
               && genericTypeArgs.Length == 1
               && typeof(T).IsAssignableFrom(genericTypeArgs[0]);
    }

    /// <summary>
    /// Checks  the type is a single element type enumeration.
    /// </summary>
    /// <param name="type">The source type object, required.</param>
    public static bool IsSingleElementTypeEnumeration(this Type type)
    {
        return
            // Arrays
            type.IsArray 
            && type.GetArrayRank() == 1
            && !type.GetElementType()!.IsArray 
            // Generics
            || type.IsGenericEnumeration()
            && type.GenericTypeArguments.Length == 1;
    }

    /// <summary>
    /// Gets the type of the elements in enumeration type, if this is a single element enumeration, Null otherwise.
    /// </summary>
    /// <param name="type">The source type object, required.</param>
    public static Type? GetSingleElementEnumerationType(this Type type)
    {
        if (type.IsSingleElementTypeEnumeration())
        {
            return type.IsArray
                ? type.GetElementType()
                : type.GenericTypeArguments.FirstOrDefault();
        }

        return null;
    }
}
