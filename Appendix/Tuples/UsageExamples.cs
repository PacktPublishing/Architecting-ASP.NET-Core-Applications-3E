using Xunit;

namespace Tuples;

public class UsageExamples
{
    [Fact]
    public void Unnamed()
    {
        var unnamed = ("some", "value", 322);
        Assert.Equal("some", unnamed.Item1);
        Assert.Equal("value", unnamed.Item2);
        Assert.Equal(322, unnamed.Item3);
    }

    [Fact]
    public void Named()
    {
        var named = (name: "Foo", age: 23);
        Assert.Equal("Foo", named.name);
        Assert.Equal(23, named.age);
    }

    [Fact]
    public void Named_equals_Unnamed()
    {
        var named = (name: "Foo", age: 23);
        Assert.Equal(named.name, named.Item1);
        Assert.Equal(named.age, named.Item2);
    }

    [Fact]
    public void ProjectionInitializers()
    {
        var name = "Foo";
        var age = 23;
        var projected = (name, age);
        Assert.Equal("Foo", projected.name);
        Assert.Equal(23, projected.age);
    }

    [Fact]
    public void TuplesEquality()
    {
        var named1 = (name: "Foo", age: 23);
        var named2 = (name: "Foo", age: 23);
        var namedDifferently = (Whatever: "Foo", bar: 23);
        var unnamed1 = ("Foo", 23);
        var unnamed2 = ("Foo", 23);

        Assert.Equal(named1, unnamed1);
        Assert.Equal(named1, named2);
        Assert.Equal(unnamed1, unnamed2);
        Assert.Equal(named1, namedDifferently);
    }

    [Fact]
    public void Deconstruction()
    {
        var tuple = (name: "Foo", age: 23);
        var (name, age) = tuple;
        Assert.Equal("Foo", name);
        Assert.Equal(23, age);
    }

    [Fact]
    public void MethodReturnValue()
    {
        var tuple1 = CreateTuple1();
        var tuple2 = CreateTuple2();
        Assert.Equal(tuple1, tuple2);

        static (string name, int age) CreateTuple1()
        {
            return (name: "Foo", age: 23);
        }

        static (string name, int age) CreateTuple2()
            => (name: "Foo", age: 23);
    }
}
