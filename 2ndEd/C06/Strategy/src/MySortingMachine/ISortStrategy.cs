using System.Collections.Generic;
using System.Linq;

namespace MySortingMachine
{
    public interface ISortStrategy
    {
        IOrderedEnumerable<string> Sort(IEnumerable<string> input);
    }
}
