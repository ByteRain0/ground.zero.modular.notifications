using Microsoft.Extensions.DependencyInjection;

namespace WebHooks.WebHooksService.Infrastructure;

public static class WebHooksManagerInstaller
{
    public static IServiceCollection AddWebHooksService(this IServiceCollection services)
    {
        return services;
    }
}
