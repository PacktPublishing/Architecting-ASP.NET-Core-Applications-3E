using Xunit;

namespace ConversionOperator;

public class Tests
{
    [Fact]
    public void Value_should_be_set_implicitly()
    {
        var value = "Test";
        SomeGenericClass<string> result = value;
        Assert.Equal("Test", result.Value);
    }

    [Fact]
    public void Value_should_be_castable()
    {
        var value = 0.5F;
        var result = (SomeGenericClass<float>)value;
        Assert.Equal(0.5F, result.Value);
        Assert.IsType<SomeGenericClass<float>>(result);
    }

    [Fact]
    public void Value_should_be_set_implicitly_using_local_function()
    {
        var result1 = GetValue("Test");
        Assert.IsType<SomeGenericClass<string>>(result1);
        Assert.Equal("Test", result1.Value);

        var result2 = GetValue(123);
        Assert.Equal(123, result2.Value);
        Assert.IsType<SomeGenericClass<int>>(result2);

        static SomeGenericClass<T> GetValue<T>(T value)
        {
            return value;
        }
    }
}

public class SomeGenericClass<T>
{
    public T? Value { get; set; }

    public static implicit operator SomeGenericClass<T>(T value)
    {
        return new SomeGenericClass<T>
        {
            Value = value
        };
    }
}
