using Microsoft.Extensions.DependencyInjection;

namespace WebHooks.ResourceAccessor.Infrastructure;

public static class WebHooksAccessorInstaller
{
    public static IServiceCollection AddWebHooksAccessor(this IServiceCollection services)
    {
        return services;
    }
}
