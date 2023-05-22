using System;
using System.Collections.Generic;

namespace MySortingMachine
{
    public sealed class SortableCollection
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
    }
}
