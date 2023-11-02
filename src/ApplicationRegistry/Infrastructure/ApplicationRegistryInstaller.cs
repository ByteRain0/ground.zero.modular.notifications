using ApplicationRegistry.Contracts;
using ApplicationRegistry.CrossCutting;
using ApplicationRegistry.Data;
using ApplicationRegistry.Routing;
using ApplicationRegistry.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Routing;

namespace ApplicationRegistry.Infrastructure;

public static class ApplicationRegistryInstaller
{
    public static IServiceCollection AddApplicationRegistryService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("Default")));
        services
            .AddScoped<IApplicationsRepository, ApplicationRepository>()
            .Decorate<IApplicationsRepository, ApplicationRepositoryCacheDecorator>();
        services.AddTransient<StatisticsService>();
        //Registering the background job example setup.
        RecurringJob.AddOrUpdate("statistics", () =>
                services.BuildServiceProvider().GetRequiredService<StatisticsService>().GatherStatisticsAsync(),
            Cron.Minutely());

        return services;
    }

    public static IApplicationBuilder ApplyApplicationModuleMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.Database.Migrate();
        return app;
    }

    public static IApplicationBuilder UseAppRegistryEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints<ApplicationsEndpoints>();
        return app;
    }
}
