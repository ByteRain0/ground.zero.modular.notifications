namespace Shared.Cache;

public interface ICacheService
{
    Task<T?> GetValueAsync<T>(string cacheKey) where T : class;

    Task SetValueAsync(string cacheKey, object value);
}
