var counter = new Counter(2);
var newCounter = counter with { Count = counter.Count + 1 };
Console.WriteLine($"Count: {counter.Count}");
Console.WriteLine($"Count: {newCounter.Count}");

public record class Counter(int Count)
{
    public bool CanCount() => true;
}
