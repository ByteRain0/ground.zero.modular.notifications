using Microsoft.Extensions.DependencyInjection;

namespace Push.Service.Infrastructure;

public static class PushManagerInstaller
{
    public static IServiceCollection AddPushService(this IServiceCollection services)
    {
        return services;
    }
}
