using System.Collections;

namespace MySortingMachine;

public sealed class SortableCollection : IEnumerable<string>
{
    public ISortStrategy? SortStrategy { get; set; }
    public IEnumerable<string> Items { get; private set; }

    public SortableCollection(IEnumerable<string> items)
    {
        Items = items;
    }

    public void Sort()
    {
        if (SortStrategy == null)
        {
            throw new NullReferenceException("Sort strategy not found.");
        }
        Items = SortStrategy.Sort(Items);
    }

    public IEnumerator<string> GetEnumerator()
        => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)Items).GetEnumerator();
}
