namespace MySortingMachine;

public class SortDescendingStrategy : ISortStrategy
{
    public IOrderedEnumerable<string> Sort(IEnumerable<string> input)
        => input.OrderByDescending(x => x);
}

public class SortDescendingStrategyClassic : ISortStrategy
{
    public IOrderedEnumerable<string> Sort(IEnumerable<string> input)
    {
        return input.OrderByDescending(x => x);
    }
}
