using Microsoft.Extensions.DependencyInjection;

namespace Shared.Cache.Output;

public static class OutputCacheInstaller
{
    public static IServiceCollection AddConfiguredOutputCache(this IServiceCollection services)
    {
        services.AddOutputCache(opts =>
        {
            opts.AddBasePolicy(policy => policy.NoCache());
            opts.AddPolicy("GetApplications", policy =>
            {
                policy.Cache()
                    .Expire(TimeSpan.FromMinutes(1))
                    .SetVaryByHeader("page", "pageSize", "sortColumn", "sortOrder")
                    .Tag("getapplications");
            });
        });

        return services;
    }
}
