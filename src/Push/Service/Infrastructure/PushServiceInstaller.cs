using Microsoft.Extensions.DependencyInjection;

namespace Push.Service.Infrastructure;

public static class PushServiceInstaller
{
    public static IServiceCollection AddPushService(this IServiceCollection services)
    {
        return services;
    }
}
