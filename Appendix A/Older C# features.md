## Older C# features

This section covers a list of C# features that are useful, less known, or I want to make sure you are aware of since we are leveraging or mentioning them in the book.

### The null-coalescing operator (C# 2.0)

The null-coalescing (??) operator is a binary operator written using the following syntax: `result = left ?? right`. It expresses to use the right value when the left value is null. Otherwise, the left value is used.

Here is a console application using the null-coalescing operator:

```csharp
Console.WriteLine(ValueOrDefault(default, "Default value"));
Console.WriteLine(ValueOrDefault("Some value", "Default value"));

static string ValueOrDefault(string? value, string defaultValue)
{
    return value ?? defaultValue;
}
```

The `ValueOrDefault` method returns `defaultValue` when `value` is null; otherwise, it returns `value`. Executing that program outputs the following:

```
Default value
Some value
```

The null-coalescing (??) operator is very convenient as it saves us from writing code like the following equivalent method:

```csharp
static string ValueOrDefaultPlain(string? value, string defaultValue)
{
    if (value == null)
    {
        return defaultValue;
    }
    return value;
}
```

Interesting Fact: C# 2.0 is also the version they added generics, which were a very welcome addition. Try to imagine C# without generics.

### Expression-bodied member (C# 6-7)

Expression-bodied members allow us to write an expression (a line of code) after the arrow operator (=>) instead of the body of that member (delimited by {}). We can write methods, properties, constructors, finalizers, and indexers this way.

Here is a small program that leverages this capability:

```csharp
Console.WriteLine(new Restaurant("The Cool Place"));
Console.WriteLine(new Restaurant("The Even Cooler Place"));

public class Restaurant
{
    public readonly string _name;
    public Restaurant(string name)
        => _name = name;
    public string Name => _name; // read-only property
    public override string ToString()
        => $"Restaurant: {Name}";
}
```

Executing the program yields:

```
Restaurant: The Cool Place
Restaurant: The Even Cooler Place
```

The equivalent with bodies would be the following code:

```csharp
public class RestaurantWithBody
{
    public readonly string _name;
    public RestaurantWithBody(string name)
    {
        _name = name;
    }
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public override string ToString()
    {
        return $"Restaurant: {Name}";
    }
}
```

As we can see from the preceding example, expression-bodied members allow us to make the code denser with less noise (less {}).

Note: I find that expression-bodied members reduce readability when the right-hand expression is complex. I rarely use expression-bodied constructors and finalizers as I find they make the code harder to read. However, read-only properties and methods can benefit from this construct as long as the right-hand expression is simple.

### Throw expressions (C# 7.0)

This feature allows us to use the throw statement as an expression, giving us the possibility to throw exceptions on the right side of the null-coalescing operator (??).

The good old-fashioned way of writing a guard clause before throw expressions was as follows:

```csharp
public HomeController(IHomeService homeService)
{
    if (homeService == null)
    {
        throw new ArgumentNullException(nameof(homeService));
    }
    _homeService = homeService;
}
```

In the preceding code, we first check for null, and if `homeService` is null, we throw an `ArgumentNullException`; otherwise, we assign the value to the field `_homeService`.

Now, with throw expressions, we can write the preceding code as a one-liner instead:

```csharp
public HomeController(IHomeService homeService)
{
    _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
}
```

Before C# 7.0, we could not throw an exception from the right side (it was a statement), but now we can (it is an expression).

---

From C# 10 onward, we can now write guards using the static `ThrowIfNull` method of the `ArgumentNullException` class like this:

```csharp
public HomeController(IHomeService homeService)
{
    ArgumentNullException.ThrowIfNull(homeService);
    _homeService = homeService;
}
```

This makes the intent a little more explicit but does not assign the value to the field, which is less than ideal for a constructor guard. If the objective is only to validate for nulls, like in a method, this new method can be handy.

---

### Tuples (C# 7.0+)

A tuple is a type that allows returning multiple values from a method or stores multiple values in a variable without declaring a type and without using the dynamic type. Since C# 7.0, tuple support has greatly improved.

**Note:** Using dynamic objects is OK in some cases, but beware that it could reduce performance and increase the number of runtime exceptions thrown due to the lack of strong types. Moreover, dynamic objects bring limited tooling support, making it harder to discover what an object can do; it is more error-prone than a strong type, there is no type checking, no auto-completion, and no compiler validation. Compile-time errors can be fixed right away without the need to wait for them to arise during runtime or worse, be reported by a user.

The C# language adds syntactic sugar regarding tuples that makes the code clearer and easier to read. Microsoft calls that lightweight syntax.

If you’ve used the `Tuple` classes before, you know that `Tuple` members are accessed through `Item1`, `Item2`, and `ItemN` properties. The `ValueTuple` struct also exposes similar fields. This newer syntax is built on top of the `ValueTuple` struct and allows us to eliminate those generic names from our codebase and replace them with meaningful user-defined ones. From now on, when referring to tuples, I refer to C# tuples, or more precisely, an instance of `ValueTuple`. If you’ve never heard of tuples, we explore them right away.

Let’s jump right into a few samples coded as xUnit tests. The first shows how we can create an unnamed tuple and access its fields using `Item1`, `Item2`, and `ItemN` which we talked about earlier:

```csharp
[Fact]
public void Unnamed()
{
    var unnamed = ("some", "value", 322);
    Assert.Equal("some", unnamed.Item1);
    Assert.Equal("value", unnamed.Item2);
    Assert.Equal(322, unnamed.Item3);
}
```

Then we can create a named tuple—very useful if you don’t like those `1`, `2`, `3` fields:

```csharp
[Fact]
public void Named()
{
    var named = (name: "Foo", age: 23);
    Assert.Equal("Foo", named.name);
    Assert.Equal(23, named.age);
}
```

Since the compiler does most of the naming and even if IntelliSense is not showing it to you, we can still access those `1`, `2`, `3` fields:

```csharp
[Fact]
public void Named_equals_Unnamed()
{
    var named = (name: "Foo", age: 23);
    Assert.Equal(named.name, named.Item1);
    Assert.Equal(named.age, named.Item2);
}
```

**Note:** If you loaded the whole Git repository, a Visual Studio analyzer should tell you not to do this by underlining those members with red error-like squiggly lines because of the configuration I’ve made in the `.editorconfig` file which instructs Visual Studio how to react to coding styles. In a default context, you should see a suggestion instead.

Moreover, we can create a named tuple using variables where names follow “magically”:

```csharp
[Fact]
public void ProjectionInitializers()
{
    var name = "Foo";
    var age = 23;
    var projected = (name, age);
    Assert.Equal("Foo", projected.name);
    Assert.Equal(23, projected.age);
}
```

Since the values are stored in those `1`, `2`, `3` fields and the programmer-friendly names are compiler-generated, equality is based on field order, not field name. Partly due to that, comparing whether two tuples are equal is pretty straightforward:

```csharp
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
```

If you don’t like to access the tuple

’s members using the dot (.) notation, we can also deconstruct them into variables:

```csharp
[Fact]
public void Deconstruction()
{
    var tuple = (name: "Foo", age: 23);
    var (name, age) = tuple;
    Assert.Equal("Foo", name);
    Assert.Equal(23, age);
}
```

Methods can also return tuples and can be used the same way that we saw in previous examples:

```csharp
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
```

**Note:** The methods are local functions, but the same applies to normal methods as well.

To conclude on tuples, I suggest avoiding them on public APIs that are exported (a shared library, for example). However, I find they come in handy internally to code helpers without creating a class that holds only data and is used once or a few times.

I think that tuples are a great addition to .NET, but I prefer fully defined types on public APIs for many reasons. The first reason is encapsulation; tuple members are fields, which breaks encapsulation. Then, accurately naming classes that are part of an API (contract/interface) is essential.

**Tip:** When you can’t find an exhaustive name for a type, the chances are that some business requirements are blurry, what is under development is not exactly what is needed, or the domain language is not clear. When that happens, try to word a clear statement about what you are trying to accomplish, and if you still can’t find a name, try to rethink that API.

For example, “I want to calculate the sales tax rate of the specified product” could yield a `CalculateSalesTaxRate(...)` method in a `Product` class or a `CalculateSalesTaxRate(Product product, ...)` in another class.

An excellent alternative to tuples for public APIs is record classes, keeping additional code minimal.

### Default literal expressions (C# 7.1)

Default literal expressions were introduced in C# 7.1 and allow us to reduce the amount of code required to use default value expressions.

Previously, we needed to write this:

```csharp
string input = default(string);
```

Or this:

```csharp
var input = default(string);
```

Now, we can write this:

```csharp
string input = default;
```

It can be very useful for optional parameters like this:

```csharp
public void SomeMethod(string input1, string input2 = default)
{
    // …
}
```

In the method defined in the preceding code block, we can pass one or two arguments to the method. When we omit the `input2` parameter, it is instantiated to `default(string)`, which is null.

We can use default literal expressions instead, which allow us to do the following:

-   Initialize a variable to its default value.
-   Set the default value of an optional method parameter.
-   Provide a default argument value to a method call.
-   Return a default value in a return statement or an expression-bodied member (the arrow `=>` operator introduced in C# 6 and 7).

Here is an example covering those use cases:

```csharp
public class DefaultLiteralExpression<T>
{
    public void Execute()
    {
        // Initialize a variable to its default value
        T? myVariable = default;
        var defaultResult1 = SomeMethod();
        // Provide a default argument value to a method call
        var defaultResult2 = SomeOtherMethod(myVariable, default);
    }

    // Set the default value of an optional method parameter
    public object? SomeMethod(T? input = default)
    {
        // Return a default value in a return statement
        return default;
    }

    // Return a default value in an expression-bodied member
    public object? SomeOtherMethod(T? input, int i) => default;
}
```

We used the generic `T` type parameter in the examples, but that could be any type. The default literal expressions become handy with complex generic types such as `Func<T>`, `Func<T1, T2>`, or tuples.

Here is a good example of how simple it is to return a tuple and return the default values of its three components using a default literal expression:

```csharp
public (object, string, bool) MethodThatReturnATuple()
{
    return default;
}
```

It is important to note that the default value of reference types (classes) is null, but the default of value types (struct) is an instance of that struct with all its fields initialized to their respective

default value. C# 10 introduces the ability to define a default parameterless constructor to value types, which initializes that struct’s default instance when using the `default` keyword, overriding the preceding assertion about default fields. Moreover, many built-in types have custom default values; for example, the default for numeric types and enum is `0`, while a `bool` is `false`.

### Switch expressions (C# 8)

This feature was introduced in C# 8 and is named switch expressions. Previously, we had to write this (code taken from the Strategy pattern code sample from Chapter 6, "Understanding the Strategy, Abstract Factory, and Singleton Design Patterns"):

```csharp
string output = default;
switch (input)
{
    case "1":
        output = PrintCollection();
        break;
    case "2":
        output = SortData();
        break;
    case "3":
        output = SetSortAsc();
        break;
    case "4":
        output = SetSortDesc();
        break;
    case "0":
        output = "Exiting";
        break;
    default:
        output = "Invalid input!";
        break;
}
```

Now, we can write this:

```csharp
var output = input switch
{
    "1" => PrintCollection(),
    "2" => SortData(),
    "3" => SetSortAsc(),
    "4" => SetSortDesc(),
    "0" => "Exiting",
    _   => "Invalid input!"
};
```

That makes the code shorter and simpler. Once you get used to it, I find this new way even easier to read. You can think about a switch expression as a switch that returns a value.

**Note:** Switch expressions also support pattern matching introduced in C# 7. C# received more pattern matching features in subsequent versions. We are not covering pattern matching here.

### Discards (C# 7)

Discards were introduced in C# 7. In the following example (code taken from the GitHub repo associated with the Strategy pattern code sample from Chapter 6, "Understanding the Strategy, Abstract Factory, and Singleton Design Patterns"), the discard became the default case of the switch (see the highlighted line):

```csharp
var output = input switch
{
    "1" => PrintCollection(),
    "2" => SortData(),
    "3" => SetSortAsc(),
    "4" => SetSortDesc(),
    "0" => "Exiting",
    _   => "Invalid input!"
};
```

Discards (`_`) are also usable in other scenarios. It is a special variable that cannot be used, a placeholder like a variable that does not exist. Using discards doesn’t allocate memory for that variable, which helps optimize your application.

It is useful when deconstructing a tuple and to use only some of its members. In the following code, we keep the reference on the `name` field but discard `age` during the deconstruction:

```csharp
var tuple = (name: "Foo", age: 23);
var (name, _) = tuple;
Console.WriteLine(name);
```

It is also very convenient when calling a method with an `out` parameter that you don’t want to use, for example:

```csharp
if (bool.TryParse("true", out _))
{
    Console.WriteLine("true was parsable!");
}
```

In that last code block, we only want to do something if the input is a Boolean, but we do not use the Boolean value itself, which is a great scenario for a discard variable.

### Async main (C# 7.1)

From C# 7.1 onward, a console application can have an async `Main` method, which is very convenient as more and more code is becoming asynchronous. This new feature allows the use of `await` directly in the `Main()` method without any quirks.

Previously, the signature of the `Main` method had to fit one of the following:

```csharp
public static void Main() { }
public static int Main() { }
public static void Main(string[] args) { }
public static int Main(string[] args) { }
```

Since C# 7.1, we can also use their async counterpart:

```csharp
public static async Task Main() { }
public static async Task<int> Main() { }
public static async Task Main(string[] args) { }
public static async Task<int> Main(string[] args) { }
```

Now, we can create a console application that looks like this:

```csharp
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Entering Main");
        var myService = new MyService();
        await myService.ExecuteAsync();
        Console.WriteLine("Exiting Main");
    }
}

public class MyService
{
    public Task ExecuteAsync()
    {
        Console.WriteLine("Inside MyService.ExecuteAsync()");
        return Task.CompletedTask;
    }
}
```

When executing the program, the result is as follows:

```text
Entering Main
Inside MyService.ExecuteAsync()
Exiting Main
```

Nothing fancy, but it allows us to take advantage of the await/async language feature directly from the Main method.

---

From .NET Core 1.0 to .NET 5, all types of applications start with a `Main` method (usually `Program.Main`), including ASP.NET Core web applications. This addition is very useful and well needed. The minimal hosting model for ASP.NET Core introduced in .NET 6 is built on top of top-level statements, introduced in .NET 5, and they make this construct implicit since the compiler generates the `Program` class and the `Main` method for us. It is still there, good to know, but chances are you won’t need to write that code manually.

---

## User-defined conversion operators (C# 1)

User-defined conversion operators are user-defined functions crafted to convert one type to another implicitly or explicitly. Many built-in types offer such conversions, such as converting an `int` to a `long` without any cast or method call:

```csharp
int var1 = 5;
long var2 = var1; // This is possible due to a class conversion operator
```

Next is an example of custom conversion. We convert a `string` to an instance of the `SomeGenericClass<string>` class without a cast:

```csharp
using Xunit;
namespace ConversionOperator;
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
```

The `SomeGenericClass<T>` class defines a generic property named `Value` that can be set to any type. The highlighted code block is the conversion operator method allowing conversion from the type `T` to `SomeGenericClass<T>` without a cast. Let’s look at the result next:

```csharp
[Fact]
public void Value_should_be_set_implicitly()
{
    var value = "Test";
    SomeGenericClass<string> result = value;
    Assert.Equal("Test", result.Value);
}
```

That first test method uses the conversion operator we just examined to convert a `string` to an instance of the `SomeGenericClass<string>` class. We can also leverage that to cast a value (a float in this case) to a `SomeGenericClass<float>` class like this:

```csharp
[Fact]
public void Value_should_be_castable()
{
    var value = 0.5F;
    var result = (SomeGenericClass<float>)value;
    Assert.Equal(0.5F, result.Value);
    Assert.IsType<SomeGenericClass<float>>(result);
}
```

Conversion operators also work with methods as the next test method will show you:

```csharp
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
```

The preceding code implicitly converts a `string` into a `SomeGenericClass<string>` object and an `int` into a `SomeGenericClass<int>` object. The highlighted line returns the value of type `T` as an instance of the `SomeGenericClass<T>` class directly; the conversion is implicit.

This is not the most important topic of the book, but if you were curious, this is how .NET does this kind of implicit conversion (like returning an instance of `T` instead of an `ActionResult<T>` in MVC controllers). Now you know that you can implement custom conversion operators in your classes too when you want that kind of behavior.

## Local functions (C# 7) and a static local function (C# 8)

In the previous example, we used a static local function new to C# 8 to demonstrate the class conversion operator.

Local functions are definable inside methods, constructors, property accessors, event accessors, anonymous methods, lambda expressions, finalizers, and other local functions. Those functions are private to their containing members. They are very useful for making the code more explicit and self-explanatory without polluting the class itself, keeping them in the consuming member’s scope. Local functions can access the declaring member’s variables and parameters like this:

```csharp
[Fact]
public void With_no_parameter_accessing_outer_scope()
{
    var x = 1;
    var y = 2;
    var z = Add();
    Assert.Equal(3, z);
    x = 2;
    y = 3;
    var n = Add();
    Assert.Equal(5, n);
    int Add()
    {
        return x + y;
    }
}
```

That is not the most robust function because the inner scope (inline function) depends on the outer scope (method variables x and y). Nonetheless, the code shows how a local function can access its parent scope’s members, which is necessary in some cases.

The following code block shows a mix of inline function scope (the y parameter) and outer scope (the x variable):

```csharp
[Fact]
public void With_one_parameter_accessing_outer_scope()
{
    var x = 1;
    var z = Add(2);
    Assert.Equal(3, z);
    x = 2;
    var n = Add(3);
    Assert.Equal(5, n);
    int Add(int y)
    {
        return x + y;
    }
}
```

That block shows how to pass an argument and how the local function can still use its outer scope’s variables to alter its result. Now, if we want an independent function decoupled from its outer scope, we could code the following instead:

```csharp
[Fact]
public void With_two_parameters_not_accessing_outer_scope()
{
    var a = Add(1, 2);
    Assert.Equal(3, a);
    var b = Add(2, 3);
    Assert.Equal(5, b);
    int Add(int x, int y)
    {
        return x + y;
    }
}
```

This code is less error-prone than the other alternatives; the logic is contained in a smaller scope (the function scope), leading to an independent inline function. But it still allows someone to alter it later and to use the outer scope, since there is nothing to tell the intent of limiting access to the outer scope, like this (some unwanted outer scope access):

```csharp
[Fact]
public void With_two_parameters_accessing_outer_scope()
{
    var z = 5;
    var a = Add(1, 2);
    Assert.Equal(8, a);
    var b = Add(2, 3);
    Assert.Equal(10, b);
    int Add(int x, int y)
    {
        return x + y + z;
    }
}
```

To clarify that intent, we can leverage static local functions. They remove the option to access the enclosing scope variables and clearly state that intent with the static keyword. The following is the static equivalent of a previous function:

```csharp
[Fact]
public void With_two_parameters()
{
    var a = Add(1, 2);
    Assert.Equal(3, a);
    var b = Add(2, 3);
    Assert.Equal(5, b);
    static int Add(int x, int y)
    {
        return x + y;
    }
}
```

Then, with that clear definition, the updated version could become the following instead, keeping the local function independent:

```csharp
[Fact]
public void With_three_parameters()
{
    var c = 5;
    var a = Add(1, 2, c);
    Assert.Equal(8, a);
    var b = Add(2, 3, c);
    Assert.Equal(10, b);
    static int Add(int x, int y, int z)
    {
        return x + y + z;
    }
}
```

Nothing can stop someone from removing the static modifier, maybe a good code review, but at least no one can say that the intent was not clear enough since the following would not compile:

```csharp
[Fact]
public void With_two_parameters_accessing_outer_scope()
{
    var z = 5;
    var a = Add(1, 2);
    Assert.Equal(8, a);
    var b = Add(2, 3);
    Assert.Equal(10, b);
    static int Add(int x, int y)
    {
        return x + y + z; // This will not compile because 'z' is outside the static local function's scope.
    }
}
```

Using the enclosing scope can be useful sometimes, but I prefer to avoid that whenever possible for the same reason that I do my best to avoid global stuff: the code can become messier faster.

To recap, we can create a local function by declaring it inside another supported member without specifying any access modifier (public, private, and so on). That function can access its declaring scope, expose parameters, and do almost everything a method can do, including being async and unsafe. Then comes C# 8, which adds the option to define a local function as static, blocking the access to its outer scope and clearly stating the intent of an independent, standalone, private local function.
