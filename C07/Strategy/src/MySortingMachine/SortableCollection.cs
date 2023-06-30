using System.Collections;
using System.Collections.Immutable;

namespace MySortingMachine;

public sealed class SortableCollection : IEnumerable<string>
{
    private ISortStrategy _sortStrategy;
    
    private ImmutableArray<string> _items;
    public IEnumerable<string> Items => _items;
    public SortableCollection(IEnumerable<string> items)
    {
        _items = items.ToImmutableArray();
        _sortStrategy = new SortAscendingStrategy();
    }

    public void SetSortStrategy(ISortStrategy strategy)
        => _sortStrategy = strategy;

    public void Sort()
    {
        _items = _sortStrategy
            .Sort(Items)
            .ToImmutableArray()
        ;
    }

    public IEnumerator<string> GetEnumerator()
        => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)Items).GetEnumerator();
}
