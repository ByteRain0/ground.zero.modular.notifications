using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.HealthChecks;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksRepository.Services.Data.Migrations;
using WebHooks.WebHooksRepository.Services.Data.Settings;

namespace WebHooks.WebHooksRepository.Services.Infrastructure;

public static class WebHooksRepositoryInstaller
{
    public static IServiceCollection AddWebHooksRepository(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<MongoDbSettings>()
            .BindConfiguration("MongoDb")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddTransient<IWebHooksRepository, WebHooksRepository>();

        services.AddHealthChecks()
            .AddMongoDb(
                name: "mongo-db",
                tags: HealthConstants.ReadinessTags,
                mongodbConnectionString: configuration["MongoDb:ConnectionString"]!);

        return services;
    }

    public static IApplicationBuilder ApplyWebHooksMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var settings = serviceScope.ServiceProvider.GetService<IOptions<MongoDbSettings>>();
        var mongoClient = new MongoClient(settings!.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

        InitialMigration.ApplyMigration(mongoDatabase, settings.Value.WebHooksCollectionName);

        return app;
    }
}
