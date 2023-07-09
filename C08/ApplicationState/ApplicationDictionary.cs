using System.Collections.Concurrent;

namespace ApplicationState;

public class ApplicationDictionary : IApplicationState
{
    private readonly ConcurrentDictionary<string, object> _memoryCache = new();

    public TItem? Get<TItem>(string key)
    {
        return _memoryCache.TryGetValue(key, out var item)
            ? (TItem)item
            : default;
    }

    public bool Has<TItem>(string key)
    {
        return _memoryCache.TryGetValue(key, out var item) && item is TItem;
    }

    public void Set<TItem>(string key, TItem value)
        where TItem : notnull
    {
        _memoryCache.AddOrUpdate(key, value, (k, v) => value);
    }
}
