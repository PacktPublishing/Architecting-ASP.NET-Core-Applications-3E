using Xunit;

namespace Singleton;

public class MySingletonWithLockTest
{
    [Fact]
    public void Create_should_always_return_the_same_instance()
    {
        var first = MySingletonWithLock.Create();
        var second = MySingletonWithLock.Create();
        Assert.Same(first, second);
    }

    [Fact]
    public void Create_should_not_return_null()
    {
        var instance = MySingletonWithLock.Create();
        Assert.NotNull(instance);
    }

}
