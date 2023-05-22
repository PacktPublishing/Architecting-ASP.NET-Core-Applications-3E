
var counter = new Counter { Count = 2 };
Console.WriteLine($"Hello, Counter: {counter.Count}!");

public class Counter
{
    public int Count { get; init; }
}
