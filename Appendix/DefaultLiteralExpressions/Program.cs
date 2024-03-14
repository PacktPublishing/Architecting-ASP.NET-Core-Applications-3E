Console.Write("See class bellow...");

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

