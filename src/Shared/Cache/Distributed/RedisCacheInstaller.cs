using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Shared.Cache.Distributed;

public static class RedisCacheInstaller
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration["Redis:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Redis connection string not specified");
            }
            return ConnectionMultiplexer.Connect(connectionString);
        });

        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
