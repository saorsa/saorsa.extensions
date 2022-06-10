using System.Collections;
using System.Runtime.CompilerServices;

namespace Saorsa;

public static class TypeUtilityExtensions
{
    public static bool IsNullable(this Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }
    
    public static Type GetUnderlyingTypeIfNullable(this Type type)
    {
        return Nullable.GetUnderlyingType(type) ?? type;
    }
    
    public static bool IsStruct(this Type type)
    {
        return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
    }
    
    public static bool IsGenericEnumeration(this Type type)
    {
        return type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type);
    }
    
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
