using Xunit;

namespace Singleton
{
    public class MySimpleSingletonTest
    {
        [Fact]
        public void Create_should_always_return_the_same_instance()
        {
            var first = MySimpleSingleton.Instance;
            var second = MySimpleSingleton.Instance;
            Assert.Same(first, second);
        }
    }
}
