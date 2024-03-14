// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class Empty(string firstName, string lastName)
{
}

public class Person(string firstName, string lastName) // <-- Primary constructor
{
    public string FirstName { get; } = firstName; // <-- Assign constructor parameter firstName to the FirstName property.
    public string LastName { get; } = lastName; // <-- Assign constructor parameter lastName to the LastName property.
}

public class MyController(IService myService)
{
    private readonly IService _myService = myService;
}

public interface IService { }

public class GuardedPerson(string firstName, string lastName)
{
    public string FirstName { get; } = firstName ?? throw new ArgumentNullException(nameof(firstName));
    public string LastName { get; } = lastName ?? throw new ArgumentNullException(nameof(lastName));
}