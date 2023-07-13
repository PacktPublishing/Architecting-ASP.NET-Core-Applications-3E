using System.Collections.Concurrent;

namespace Wishlist;

public class InMemoryWishListRefactored : IWishList
{
    private readonly InMemoryWishListOptions _options;
    private readonly ConcurrentDictionary<string, InternalItem> _items = new();

    public InMemoryWishListRefactored(InMemoryWishListOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public Task<WishListItem> AddOrRefreshAsync(string itemName)
    {
        RemoveExpiredItems();
        var item = _items.AddOrUpdate(
            itemName,
            AddValueFactory,
            UpdateValueFactory
        );
        return MapAsync(itemName, item);
    }

    public Task<IEnumerable<WishListItem>> AllAsync()
    {
        RemoveExpiredItems();
        var items = _items
            .Select(x => Map(x.Key, x.Value))
            .OrderByDescending(x => x.Count)
            .AsEnumerable()
        ;
        return Task.FromResult(items);
    }

    private DateTimeOffset GetExpirationTime()
        => _options.SystemClock.UtcNow.AddSeconds(_options.ExpirationInSeconds);

    private InternalItem AddValueFactory(string key)
        => new(Count: 1, Expiration: GetExpirationTime());

    private InternalItem UpdateValueFactory(string key, InternalItem item)
        => item with { Count = item.Count + 1, Expiration = GetExpirationTime() };

    private static Task<WishListItem> MapAsync(string itemName, InternalItem item)
        => Task.FromResult(Map(itemName, item));

    private static WishListItem Map(string itemName, InternalItem item)
        => new(
            Name: itemName,
            Count: item.Count,
            Expiration: item.Expiration
        );

    private void RemoveExpiredItems()
    {
        _items
            .Where(x => x.Value.Expiration < _options.SystemClock.UtcNow)
            .Select(x => x.Key)
            .ToList()
            .ForEach(key => _items.Remove(key, out _))
        ;
    }

    private record class InternalItem(int Count, DateTimeOffset Expiration);
}
