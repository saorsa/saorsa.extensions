using System;
using System.Collections.Generic;
using System.Linq;

namespace Saorsa.Extensions.Tests;

public class TypeUtilityExtensions
{
    private static readonly Type[] NullableTypes =
    {
        typeof(int?),
        typeof(byte?),
        typeof(DateTime?),
        typeof(TimeOnly?),
    };
    
    private static readonly Type[] SimpleTypes =
    {
        typeof(short),
        typeof(int),
        typeof(long),
        typeof(ushort),
        typeof(uint),
        typeof(ulong),
        typeof(char),
        typeof(byte),
        typeof(sbyte),
        typeof(string),
    };

    private static readonly Type[] StructureTypes =
    {
        typeof(Guid),
        typeof(DateTime),
        typeof(DateOnly),
        typeof(DateTimeOffset),
        typeof(TimeSpan),
        typeof(TimeOnly),
        typeof(decimal),
    };

    private static readonly Type[] GenericEnumerationTypes =
    {
        typeof(List<object>),
        typeof(List<string>),
        typeof(List<byte>),
        typeof(Dictionary<object, object>),
        typeof(Dictionary<object, int>),
        typeof(HashSet<string>),
    };
    
    private static readonly Type[] NonGenericEnumerationTypes =
    {
        typeof(object[]),
        typeof(string),
        typeof(byte[])
    };

    private static readonly Type[] SingleElementEnumerationTypes =
    {
        typeof(int[]),
        typeof(List<string>),
        typeof(HashSet<string>),
    };

    private static readonly Type[] NonSingleElementEnumerationTypes =
    {
        typeof(int[][][][]),
        typeof(int[][]),
        typeof(int[,]),
        typeof(Dictionary<string, string>),
        typeof(string)
    };
    
    private class TestDummyClass
    {
    }

    private struct TestDummyStruct
    {
    }

    private enum TestDummyEnum
    {
    }

    [Test]
    public void TestCompositeTypes()
    {
        Assert.False(typeof(TestDummyClass).IsStruct());
        Assert.False(typeof(TestDummyEnum).IsStruct());
        Assert.True(typeof(TestDummyStruct).IsStruct());
    }
    
    [Test]
    public void TestStructureTypes()
    {
        foreach (var st in StructureTypes)
        {
            Assert.True(st.IsStruct(),
                $"Type '{st.Name}' is expected to be a structure.");
        }
    }

    [Test]
    public void TestNonStructureTypes()
    {
        foreach (var st in SimpleTypes)
        {
            Assert.False(st.IsStruct(),
                $"Type '{st.Name}' is NOT a structure.");
        }
    }

    [Test]
    public void TestIsGenericEnumeration()
    {
        GenericEnumerationTypes.ToList().ForEach(t =>
        {
            Assert.True(t.IsGenericEnumeration(),
                $"Type '{t}' is expected to be generic enumeration.");
        });
        
        NonGenericEnumerationTypes.ToList().ForEach(t =>
        {
            Assert.False(t.IsGenericEnumeration(),
                $"Type '{t}' is NOT a generic enumeration.");
        });
    }
    
    [Test]
    public void TestEnumerationTypes()
    {
        SingleElementEnumerationTypes.ToList().ForEach(t =>
        {
            Assert.True(t.IsSingleElementTypeEnumeration(),
                $"Type '{t}' is expected to be single element type enumeration.");
        });
        
        NonSingleElementEnumerationTypes.ToList().ForEach(t =>
        {
            Assert.False(t.IsSingleElementTypeEnumeration(),
                $"Type '{t}' is NOT a single element type enumeration.");
        });
    }

    [Test]
    public void TestIsNullable()
    {
        NullableTypes.ToList().ForEach(t =>
        {
           Assert.True(t.IsNullable(),
               $"Type '{t}' is expected as nullable."); 
        });
    }
    
    
    [Test]
    public void TestGetUnderlyingTypeIfNullable()
    {
        NullableTypes.ToList().ForEach(t =>
        {
            var expectedUnderlyingType = Nullable.GetUnderlyingType(t);
            var underlyingType = t.GetUnderlyingTypeIfNullable();

            Assert.AreEqual(expectedUnderlyingType, underlyingType);
        });


        SimpleTypes
            .Concat(StructureTypes)
            .Concat(GenericEnumerationTypes)
            .Concat(NonGenericEnumerationTypes)
            .Concat(SingleElementEnumerationTypes)
            .Concat(NonSingleElementEnumerationTypes)
            .ToList()
            .ForEach(t =>
            {
                var underlyingType = t.GetUnderlyingTypeIfNullable();
                Assert.AreEqual(t, underlyingType);
            });
    }
}
