using System.Collections.Concurrent;

namespace ApplicationState;

public class ApplicationDictionary : IApplicationState
{
    private readonly ConcurrentDictionary<string, object> _cache = new();

    public TItem? Get<TItem>(string key)
    {
        return _cache.TryGetValue(key, out var item)
            ? (TItem)item
            : default;
    }

    public bool Has<TItem>(string key)
    {
        return _cache.TryGetValue(key, out var item) && item is TItem;
    }

    public void Set<TItem>(string key, TItem value)
        where TItem : notnull
    {
        _cache.AddOrUpdate(key, value, (k, v) => value);
    }
}
