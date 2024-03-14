# What’s new in .NET 7-8 and C# 11-12?

On top of performance improvements, .NET 7-8 and C# 11-12 have brought many new features. We visit some of them here.

In this section, we explore the following C# features:

-   Raw string literals (C# 11)
-   Random (C# 12)
-   The required modifier (C# 11)
-   Primary Constructor (C# 12)

## Raw string literals

Raw string literals allow us to write multiline strings that are indented with the rest of our code. A raw string literal starts and ends with `"""`. Let's look at the following Fact as an example:

```csharp
public class RawStringLiteralTests
{
    [Fact]
    public void The_whitespaces_are_not_considered()
    {
        var rawStringLiteral = """
            This
            is
            a
            raw
            string
            literal
            """;
        var normalString = $"This{Environment.NewLine}is{Environment.NewLine}a{Environment.NewLine}raw{Environment.NewLine}string{Environment.NewLine}literal";
        Assert.Equal(normalString, rawStringLiteral);

        var multiLineString = @"This
is
a
raw
string
literal";
        Assert.Equal(multiLineString, rawStringLiteral);
    }
}
```

The `rawStringLiteral` variable (highlighted) is indented. The `normalString` variable contains the same text. It uses the `Environment.NewLine` property to ensure the line breaks are correct on both Windows (`\r\n`) and Unix-based (`\n`) systems. We can also compare with the pre-C# 11 multiline string, the `multiLineString` variable, which forces us to mis-indent it.

We can use quotes in raw literal string without any special character like this:

```csharp
var rawStringLiteral = """
    This is a raw string literal
    with "quotes"!
    """;
```

We can also combine raw literal strings with interpolation like this:

```csharp
var variable = 123;
var rawStringLiteral = $"""
    This is a raw string literal
    with a variable ({variable}) inside!
    """;
```

If we must use curly braces in the string, we can also prefix the raw literal string with more than one `$` sign like this:

```csharp
var rawStringLiteral = $$"""
    This is a raw string literal
    with a variable ({{variable}}) inside!
    """;
var rawStringLiteral = $$$"""
    This is a raw string literal
    with a variable ({{{variable}}}) inside!
    """;
```

This is a great addition to C#, especially when writing code manually, like JSON.

## Random

With .NET 8, the `Random` and `RandomNumberGenerator` (System.Security.Cryptography) classes expose the generic `GetItems` and `Shuffle` methods that get a random item from a collection and randomize a collection, respectively.

The following code prints a random word from the Lorem Ipsum string:

```csharp
var items = "Lorem ipsum dolor sit amet".Split(' ');
var results = Random.Shared.GetItems(items, 1);
Console.WriteLine(results.Single());
```

The following code shuffles the array and outputs the resulting shuffled sentence:

```csharp
Random.Shared.Shuffle(items);
Console.WriteLine(string.Join(' ', items));
```

I remember writing code to achieve similar results many times, but now that time is over!

## The required modifier

The `required` modifier allows us to force the initialization of properties and fields. This allows us to define a parameterless constructor, for example, yet requires that certain properties be initialized using an object initializer.

For example, let’s define a class:

```csharp
public class Entity
{
    public required string Name { get; set; }
}
```

The `required` modifier is applied to the `Name` property of the `Entity` class. That means the following code results in a compilation error:

```csharp
var entity = new Entity();
```

We must initialize the `Name` property like this:

```csharp
var entity = new Entity()
{
    Name = "Test"
};
```

This is a very convenient modifier that enables different scenarios that were hard to do before.

See the official C# Reference required modifier page (https://adpg.link/EAw9) for more information.

## Primary Constructor

C# 12 allows us to use primary constructors for non-record types. Primary constructors are a streamlined way to initialize a class or struct, reducing the boilerplate code associated with property and field initialization. This feature is especially handy when the constructor simply assigns values to properties or fields.

A primary constructor is defined directly in the class or struct declaration and allows parameters to be directly assigned to properties or fields. You do not have to use the parameters and unlike record types, the parameters do not become properties automatically. Here's an example:

```csharp
public class Empty(string firstName, string lastName)
{
}
```

The preceding `Empty` class contains no properties, only a constructor with two parameters (`firstName` and `lastName`) that does nothing; but the code compiles.

### Assign constructor arguments to properties

However, we can assign constructor arguments directly to properties, like this:

```csharp
public class Person(string firstName, string lastName) // <-- Primary constructor
{
    public string FirstName { get; } = firstName; // <-- Assign constructor parameter firstName to the FirstName property.
    public string LastName { get; } = lastName; // <-- Assign constructor parameter lastName to the LastName property.
}
```

The preceding `Person` class has a primary constructor with two parameters, `firstName` and `lastName`, which are assigned to the `FirstName` and `LastName` properties.

### Assign constructor arguments to fields

Similarly, primary constructors can be used to assign arguments directly to fields. Here's an example:

```csharp
public class MyController(IService myService)
{
    private readonly IService _myService = myService;
}
```

The preceding `MyController` class has a primary constructor with a `myService` parameter that is assigned to a private field `_myService`. This pattern is handy for cases where direct access to the property is not required or when encapsulation is preferred, like when injecting a dependency.

### Guarding against Null

Primary constructors also allow you to include guard clauses to ensure that required parameters are not null, enhancing the robustness of your code. This can be done directly within the constructor parameter list using the `required` modifier or by employing null checks within the constructor body.

```csharp
public class GuardedPerson(string firstName, string lastName)
{
    public string FirstName { get; } = firstName ?? throw new ArgumentNullException(nameof(firstName));
    public string LastName { get; } = lastName ?? throw new ArgumentNullException(nameof(lastName));
}
```

In the above example, the `GuardedPerson` class has a primary constructor with `firstName` and `lastName` parameters. The values are assigned to the `FirstName` and `LastName` properties and by using throw expressions, if a parameter is null, an `ArgumentNullException` will be thrown.

### Conclusion

Primary constructors streamline the initialization of objects by allowing direct assignment to properties and fields and supports the implementation of guard clauses. They can help reduce boilerplate code, yet as a newer construct, may make the code harder to read for unfamiliar eyes.
