using Wishlist.Internal;

namespace Wishlist;

public class InMemoryWishListOptions
{
    public ISystemClock SystemClock { get; set; } = new SystemClock();
    public int ExpirationInSeconds { get; set; } = 30;
}
