using Microsoft.Extensions.DependencyInjection;

namespace WebHooks.WebHooksService.Infrastructure;

public static class WebHooksServiceInstaller
{
    public static IServiceCollection AddWebHooksService(this IServiceCollection services)
    {
        return services;
    }
}
