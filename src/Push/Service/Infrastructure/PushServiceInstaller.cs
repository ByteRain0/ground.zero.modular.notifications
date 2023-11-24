using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Push.SubscriptionFilter;

namespace Push.Service.Infrastructure;

public static class PushServiceInstaller
{
    public static IServiceCollection AddPushService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<PushService>();

        services.AddTransient<ISubscriptionService, SubscriptionService>();

        services.AddFeatureManagement()
            .AddFeatureFilter<SubscriptionCreditsFilter>();

        return services;
    }
}
