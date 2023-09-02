using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksRepository.Services.Data.Settings;

namespace WebHooks.WebHooksRepository.Services.Infrastructure;

public static class WebHooksRepositoryInstaller
{
    public static IServiceCollection AddWebHooksRepository(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO: find a way to validate the options.
        var mongoDbSettings = new MongoDbSettings
        {
            ConnectionString = configuration.GetConnectionString("MongoDb")!,
            DatabaseName = "WebHooks",
            WebHooksCollectionName = "WebHooks"
        };

        services.AddSingleton(Options.Create(mongoDbSettings));

        services.AddTransient<IWebHooksRepository, WebHooksRepository>();
        return services;
    }
}
