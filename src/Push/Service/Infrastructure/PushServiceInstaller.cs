using Microsoft.Extensions.DependencyInjection;

namespace Push.Service.Infrastructure;

public static class PushServiceInstaller
{
    public static IServiceCollection AddPushService(this IServiceCollection services)
    {
        services.AddScoped<PushService>();
        return services;
    }
}
