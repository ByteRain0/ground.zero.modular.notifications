using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Shared.Cache;

public static class RedisCacheInstaller
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration.GetConnectionString("Redis");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Redis connection string not specified");
            }
            return ConnectionMultiplexer.Connect(connectionString);
        });

        return services;
    }
}
