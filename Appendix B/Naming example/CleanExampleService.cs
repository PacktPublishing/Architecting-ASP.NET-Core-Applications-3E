using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ClearName
{
    public class CleanExampleService : IExampleService
    {
        private readonly IEnumerable<string> _data;
        private static readonly Random _random = new Random();

        public CleanExampleService(IEnumerable<string> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            if (_data.Count() > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(data), 
                    $"The number of elements must be lower or equal to '{byte.MaxValue}'."
                );
            }
        }

        public RandomResult RandomizeOneString()
        {
            var upperBound = _data.Count();
            var index = _random.Next(0, upperBound);
            var shuffledData = ShuffleData();
            var randomString = shuffledData.ElementAt(index);
            return new RandomResult(randomString, index, shuffledData);
        }

        private IEnumerable<string> ShuffleData()
        {
           return _data
                .Select(value => new { Value = value, Order = _random.NextDouble() })
                .OrderBy(x => x.Order)
                .Select(x => x.Value)
            ;
        }
    }
}
