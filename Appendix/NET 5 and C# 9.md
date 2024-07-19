# What’s new in .NET 5 and C# 9?

In this section we explore the following C# 9 features:

-   Top-level statements
-   Target-typed new expressions
-   Init-only properties
-   Record classes

We use top-level statements to simplify code samples leading to one code file with less boilerplate code. Moreover, top-level statements are the building blocks of the .NET 6 minimal hosting model and minimal APIs. We dig into the new expressions which allow creating new instances with less typing. The init-only properties are the backbone of the record classes used in multiple chapters and are foundational to the MVU example presented in Chapter 18, A Brief Look into Blazor.

## Top-level statements

Starting from C# 9, it is possible to write statements before declaring namespaces and other members. Those statements are compiled to an emitted `Program.Main` method.

With top-level statements, a minimal .NET “Hello World” program now looks like this:

```csharp
using System;
Console.WriteLine("Hello world!");
```

Unfortunately, we also need a project to run, so we have to create a `.csproj` file with the following content:

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>net5.0</TargetFramework>
        <OutputType>Exe</OutputType>
    </PropertyGroup>
</Project>
```

From there, we can use the .NET CLI to `dotnet run` the application.

> **Note:** I left the `TargetFramework` as `net5.0` because this is related to .NET 5. We revisit top-level statements in the What’s new in .NET 6 and C# 10? section.

We can also declare other members like classes and use them as in any other application. Classes must be declared after the top-level code. Be aware that the top-level statement code is not part of any namespace and it is recommended to create classes in a namespace, so you should limit the number of declarations done in the `Program.cs` file to what is internal to its inner workings.

Top-level statements are a great feature for getting started with C# and writing code samples by cutting out boilerplate code.

## Target-typed new expressions

Target-typed new expressions are a new way of initializing types. C# 3 introduced the `var` keyword back in the day, which became very handy to work with generic types, LINQ return values, and more (I remember embracing that new construct with joy).

This new C# feature does the opposite of the `var` keyword by letting us call the constructor of a known type like this:

```csharp
List<string> list1 = new();
List<string> list2 = new(10);
List<string> list3 = new(capacity: 10);

var obj = new MyClass(new());
AnotherClass anotherObj = new() { Name = "My Name" };

public class MyClass
{
    public MyClass(AnotherClass property)
        => Property = property;

    public AnotherClass Property { get; }
}

public class AnotherClass
{
    public string? Name { get; init; }
}
```

The first highlight shows the ability to create new objects when the type is known using the `new()` keyword and omitting the type name. The second list is created the same way but we passed the argument `10` to its constructor. The third list uses the same approach but explicitly specifies the parameter name as we could with any standard constructor. Using a named parameter makes the code easier to understand.

The instance of `MyClass` assigned to the `obj` variable is created explicitly, but `new()` is used to create an instance of `AnotherClass`, which is inferred because the parameter type is known.

The final example demos the use of class initializers. As you may have noticed, the `AnotherClass` class has an init-only property, which is our next subject.

I can see the target-typed new expressions simplify many codebases. I started using them, and they are a great addition to C# 9.0. Please be careful not to make your code harder to read by abusing target-typed new expressions; only use them when the type is clear, like `MyType variable = new()`.

## Init-only properties

Init-only properties are read-only properties that can be initialized using class initializers. Previously, read-only properties could only be initialized in the constructor or with property initializers (such as `public int SomeProp { get; } = 2;`).

For example, let’s take a class that holds the state of a counter. A read-only property would look like `Count`:

```csharp
public class Counter
{
    public int Count { get; }
}
```

Without a constructor, it is impossible to initialize the `Count` property, so we can’t initialize an instance like this:

```csharp
var counter = new Counter { Count = 2 };
```

That’s the use case that init-only properties enable. We can rewrite the `Counter` class to make use of that by using the `init` keyword like this:

```csharp
public class Counter
{
    public int Count { get; init; }
}
```

With that in place, we can now use it like this:

```csharp
var counter = new Counter { Count = 2 };
Console.WriteLine($"Hello Counter: {counter.Count}!");
```

Init-only properties enable developers to create immutable properties that are settable using a class initializer. They are also a building block of record classes.

## Record classes

A record class uses init-only properties and allows making reference types (classes) immutable. The only way to change a record is to create a new one. Let’s convert the `Counter` class into a record:

```csharp
public record Counter
{
    public int Count { get; init; }
}
```

Yes, it is as simple as replacing the `class` keyword with the `record` keyword. Since .NET 6, we can keep the `class` keyword as well to differentiate (and make consistent) the new record struct like this:

```csharp
public record class Counter
{
    public int Count { get; init; }
}
```

But that’s not all:

-   We can simplify record creation.
-   We can also use the `with` keyword to simplify “mutating” a record (creating a mutated copy without changing the source).
-   Records support deconstruction like the tuple types.
-   .NET auto-implements the `Equals` and `GetHashCode` methods. Those two methods compare the value of the properties instead of the reference to the object. That means that two different instances with equal values would be equal.
-   .NET auto-overrides the `ToString` method that outputs a better format including property values.

All in all, that means we end up with an immutable reference type (class) that behaves like a value type (struct) without the copy allocation cost.

### Simplifying the record creation

If we don’t want to use a class initializer when creating instances, we can simplify the code of our records by using primary constructors, like the following:

```csharp
public record class Counter(int Count);
```

> **Note:** That syntax reminds me of TypeScript, where you can define fields in the constructor, and they get implemented automatically without the need to write any plumbing code.

Then we can create a new instance like with any other class:

```csharp
var counter = new Counter(2);
Console.WriteLine($"Count: {counter.Count}");
```

Running that code would output `Count: 2` in the console. We can also add methods to the record class:

```csharp
public record class Counter(int Count)
{
    public bool CanCount() => true;
}
```

You can do everything with a record that you would do with a class and more. The record class is a class like any other.

### The with keyword

The `with` keyword allows us to create a copy of a record and change only the value of certain properties without altering the others. Let’s take a look at the following code:

```csharp
var initialDate = DateTime.UtcNow.AddMinutes(-1);
var initialForecast = new Forecast(initialDate, 20, "Sunny");
var currentForecast = initialForecast with { Date = DateTime.UtcNow };
Console.WriteLine(initialForecast);
Console.WriteLine(currentForecast);

public record class Forecast(DateTime Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
```

When we execute that code, we end up with a result similar to this:

```
Forecast { Date = 9/22/2020 12:04:20 AM, TemperatureC = 20, Summary = Sunny, TemperatureF = 67 }
Forecast { Date = 9/22/2020 12:05:20 AM, TemperatureC = 20, Summary = Sunny, TemperatureF = 67 }
```

The power of the `with` keyword allows us to create a copy of the `initialForecast` record and only change the `Date` property’s value.

> **Note:** The formatted output is provided by the overloaded `ToString` method that comes by default with record classes. We have nothing to do to make this happen.

The `with` keyword is a very compelling addition to the language.

### Deconstruction

We can deconstruct record classes like a tuple:

```csharp
var current = new Forecast(DateTime.UtcNow, 20, "Sunny");
var (date, temperatureC, summary) = current;
Console.WriteLine($"date: {date}");
Console.WriteLine($"temperatureC: {temperatureC}");
Console.WriteLine($"summary: {summary}");

public record class Forecast(DateTime Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);


}
```

By default, all positional members (defined in the constructor) are deconstructable. In that example, we cannot access the `TemperatureF` property by using deconstruction because it is not a positional member.

We can create a custom deconstructor by implementing one or more `Deconstruct` methods that expose `out` parameters of the properties that we want to be deconstructable like this:

```csharp
using System;
var current = new Forecast(DateTime.UtcNow, 20, "Sunny");
var (date, temperatureC, summary, temperatureF) = current;
Console.WriteLine($"date: {date}");
Console.WriteLine($"temperatureC: {temperatureC}");
Console.WriteLine($"summary: {summary}");
Console.WriteLine($"temperatureF: {temperatureF}");

public record Forecast(DateTime Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public void Deconstruct(out DateTime date, out int temperatureC, out string summary, out int temperatureF)
    => (date, temperatureC, summary, temperatureF) = (Date, TemperatureC, Summary, TemperatureF);
}
```

With that updated sample, we can also access the `TemperatureF` property’s value when deconstructing the record.

Lastly, by adding `Deconstruct` methods, we can control the way our record classes get deconstructed.

### Equality comparison

As mentioned previously, the default comparison between two records is made by their values and not their memory addresses, so two different instances with the same values are equal. The following code proves this:

```csharp
var employee1 = new Employee("Johnny", "Mnemonic");
var employee2 = new Employee("Clark", "Kent");
var employee3 = new Employee("Johnny", "Mnemonic");
Console.WriteLine($"Does '{employee1}' equals '{employee2}'? {employee1 == employee2}");
Console.WriteLine($"Does '{employee1}' equals '{employee3}'? {employee1 == employee3}");
Console.WriteLine($"Does '{employee2}' equals '{employee3}'? {employee2 == employee3}");

public record Employee(string FirstName, string LastName);
```

When running that code, the output is as follows:

```
Does 'Employee { FirstName = Johnny, LastName = Mnemonic }' equals 'Employee { FirstName = Clark, LastName = Kent }'? False
Does 'Employee { FirstName = Johnny, LastName = Mnemonic }' equals 'Employee { FirstName = Johnny, LastName = Mnemonic }'? True
Does 'Employee { FirstName = Clark, LastName = Kent }' equals 'Employee { FirstName = Johnny, LastName = Mnemonic }'? False
```

In that example, even if `employee1` and `employee3` are two different objects, the result is true when we compare them using `employee1 == employee3`, proving that values were compared, not instances.

Once again, we leveraged the `ToString()` method of record classes, which is returning a developer-friendly representation of its data. The `ToString()` method of an object is called implicitly when using string interpolation, like in the preceding code block, hence the complete output.

On the other hand, if you want to know if they are the same instance, you can use the `object.ReferenceEquals()` method like this:

```csharp
Console.WriteLine($"Is 'employee1' the same as 'employee3'? {object.ReferenceEquals(employee1, employee3)}");
```

This will output the following:

```
Is 'employee1' the same as 'employee3'? False
```

## Conclusion

Record classes are a great new addition that creates immutable types in a few keystrokes. Furthermore, they support deconstruction and implement equality comparison that compares the value of properties, not whether the instances are the same, simplifying our lives in many cases.

Init-only properties can also benefit regular classes if one prefers class initializers to constructors.

```

```
