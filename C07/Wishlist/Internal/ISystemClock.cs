namespace Wishlist.Internal;

public interface ISystemClock
{
    DateTimeOffset UtcNow { get; }
}
