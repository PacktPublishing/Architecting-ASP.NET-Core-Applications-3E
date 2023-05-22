using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ClearName
{
    public class OneMethodExampleService : IExampleService
    {
        private readonly IEnumerable<string> _data;
        private static readonly Random _random = new Random();

        public OneMethodExampleService(IEnumerable<string> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public RandomResult RandomizeOneString()
        {
            // Find the upper bound
            var upperBound = _data.Count();

            // Randomly select the index of the string to return
            var index = _random.Next(0, upperBound);

            // Shuffle the elements to add more randomness
            var shuffledList = _data
                .Select(value => new { Value = value, Order = _random.NextDouble() })
                .OrderBy(x => x.Order)
                .Select(x => x.Value)
            ;

            // Return the randomly selected element
            var randomString = shuffledList.ElementAt(index);
            return new RandomResult(randomString, index, shuffledList);
        }
    }
}
