
using System.Diagnostics.CodeAnalysis;

var obj = Create(true);
Console.WriteLine($"Hello, {obj?.Name}!");

static MyClass? Create(bool shouldYieldANullResult)
{
    return shouldYieldANullResult
        ? default
        : new()
    ;
}

public class MyClass
{
    public string? Name { get; set; }
}