using Microsoft.Extensions.DependencyInjection;

namespace WebHooks.WebHooksRepository.Infrastructure;

public static class WebHooksAccessorInstaller
{
    public static IServiceCollection AddWebHooksRepository(this IServiceCollection services)
    {
        return services;
    }
}
