using System.Collections;

namespace Saorsa;

public static class TypeUtilityExtensions
{
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
}
