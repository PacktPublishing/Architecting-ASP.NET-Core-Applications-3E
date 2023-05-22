using System;
using Xunit;

namespace Singleton
{
    public class MySingletonTest
    {
        [Fact]
        public void Create_should_always_return_the_same_instance()
        {
            var first = MySingleton.Create();
            var second = MySingleton.Create();
            Assert.Same(first, second);
        }
    }
}
