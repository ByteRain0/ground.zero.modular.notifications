using System.Text.Json;
using Microsoft.Extensions.Logging;
using Shared.Constants;
using StackExchange.Redis;

namespace Shared.Cache.Distributed;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer, ILogger<RedisCacheService> logger)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _logger = logger;
    }

    public async Task<T?> GetValueAsync<T>(string cacheKey) where T : class
    {
        var db = _connectionMultiplexer.GetDatabase();
        var cachedApplication = await db.StringGetAsync(cacheKey);
        return !cachedApplication.IsNullOrEmpty
            ? JsonSerializer.Deserialize<T>(cachedApplication.ToString())
            : null;
    }

    public async Task SetValueAsync(string cacheKey, object value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var wasCachedSuccessfully = await db.StringSetAsync(
            key: new RedisKey(cacheKey),
            value: new RedisValue(JsonSerializer.Serialize(value)),
            flags: CommandFlags.FireAndForget);

        if (!wasCachedSuccessfully)
        {
            _logger.LogError(LogEvents.CacheSaveFailure.EventId, LogEvents.CacheSaveFailure.Message);
        }
    }
}
