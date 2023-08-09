using Microsoft.Extensions.DependencyInjection;

namespace Push.Infrastructure;

public static class PushManagerInstaller
{
    public static IServiceCollection AddPushService(this IServiceCollection services)
    {
        return services;
    }
}
