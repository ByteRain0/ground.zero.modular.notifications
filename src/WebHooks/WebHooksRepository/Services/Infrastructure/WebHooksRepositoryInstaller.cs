using Microsoft.Extensions.DependencyInjection;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksRepository.Services.Data.Settings;

namespace WebHooks.WebHooksRepository.Services.Infrastructure;

public static class WebHooksRepositoryInstaller
{
    public static IServiceCollection AddWebHooksRepository(this IServiceCollection services)
    {
        services.AddOptions<MongoDbSettings>()
            .BindConfiguration("MongoDb")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddTransient<IWebHooksRepository, WebHooksRepository>();
        return services;
    }
}
