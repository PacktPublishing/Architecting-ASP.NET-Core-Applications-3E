namespace Wishlist.Internal;

public class CustomClock : ISystemClock
{
    private readonly TimeProvider _timeProvider;
    public CustomClock(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }
    public DateTimeOffset UtcNow => _timeProvider.GetUtcNow();
}

public class SystemClock : ISystemClock
{
    // Same as: public DateTimeOffset UtcNow => TimeProvider.System.GetUtcNow();
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
