using Microsoft.Extensions.DependencyInjection;
using WebHooks.WebHooksRepository.Contracts;

namespace WebHooks.WebHooksRepository.Services.Infrastructure;

public static class WebHooksRepositoryInstaller
{
    public static IServiceCollection AddWebHooksRepository(this IServiceCollection services)
    {
        services.AddTransient<IWebHooksRepository, WebHooksRepository>();
        return services;
    }
}
