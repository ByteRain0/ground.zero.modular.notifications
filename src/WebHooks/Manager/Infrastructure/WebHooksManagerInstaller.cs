using Microsoft.Extensions.DependencyInjection;

namespace WebHooks.Manager.Infrastructure;

public static class WebHooksManagerInstaller
{
    public static IServiceCollection AddWebHooksManager(this IServiceCollection services)
    {
        return services;
    }
}
