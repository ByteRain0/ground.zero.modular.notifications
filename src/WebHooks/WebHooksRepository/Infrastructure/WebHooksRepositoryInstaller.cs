using Microsoft.Extensions.DependencyInjection;

namespace WebHooks.WebHooksRepository.Infrastructure;

public static class WebHooksRepositoryInstaller
{
    public static IServiceCollection AddWebHooksRepository(this IServiceCollection services)
    {
        return services;
    }
}
