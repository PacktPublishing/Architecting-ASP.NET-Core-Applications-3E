using System.Collections.Concurrent;

namespace ApplicationState;

public class ApplicationDictionary : IApplicationState
{
    private readonly ConcurrentDictionary<string, object> _memoryCache = new();

    public TItem? Get<TItem>(string key)
    {
        if (!Has<TItem>(key))
        {
            return default;
        }
        return (TItem)_memoryCache[key];
    }

    public bool Has<TItem>(string key)
    {
        return _memoryCache.ContainsKey(key) && _memoryCache[key] is TItem;
    }

    public void Set<TItem>(string key, TItem value)
        where TItem : notnull
    {
        _memoryCache[key] = value;
    }
}
