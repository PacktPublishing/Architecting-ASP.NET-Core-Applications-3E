using Microsoft.Extensions.Caching.Memory;

namespace ApplicationState;

public class ApplicationMemoryCache : IApplicationState
{
    private readonly IMemoryCache _memoryCache;

    public ApplicationMemoryCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public TItem Get<TItem>(string key)
    {
        return _memoryCache.Get<TItem>(key);
    }

    public bool Has<TItem>(string key)
    {
        return _memoryCache.TryGetValue<TItem>(key, out _);
    }

    public void Set<TItem>(string key, TItem value)
        where TItem : notnull
    {
        _memoryCache.Set(key, value);
    }
}
