using System;
using System.Collections.Generic;

namespace ClearName
{
    public class RandomResult
    {
        public RandomResult(string result, int index, IEnumerable<string> data)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
            Index = index;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public string Result { get; }
        public int Index { get; }
        public IEnumerable<string> Data { get; }
    }
}
