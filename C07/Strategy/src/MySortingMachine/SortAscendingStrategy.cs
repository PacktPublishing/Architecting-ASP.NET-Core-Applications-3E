namespace MySortingMachine;

public class SortAscendingStrategy : ISortStrategy
{
    public IOrderedEnumerable<string> Sort(IEnumerable<string> input) 
        => input.OrderBy(x => x);
}
