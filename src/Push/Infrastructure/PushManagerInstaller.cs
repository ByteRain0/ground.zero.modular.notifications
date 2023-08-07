using Microsoft.Extensions.DependencyInjection;

namespace Push.Infrastructure;

public static class PushManagerInstaller
{
    public static IServiceCollection AddPushManager(this IServiceCollection services)
    {
        return services;
    }
}
