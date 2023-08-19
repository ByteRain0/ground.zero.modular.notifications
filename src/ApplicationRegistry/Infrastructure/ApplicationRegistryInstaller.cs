using ApplicationRegistry.Contracts;
using ApplicationRegistry.CrossCutting;
using ApplicationRegistry.Data;
using ApplicationRegistry.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationRegistry.Infrastructure;

public static class ApplicationRegistryInstaller
{
    public static IServiceCollection AddApplicationRegistryService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("Default")));

        services
            .Decorate<IApplicationsRepository, ApplicationRepositoryLoggingDecorator>()
            .Decorate<IApplicationsRepository, ApplicationRepositoryCacheDecorator>()
            .AddScoped<IApplicationsRepository, ApplicationRepository>();

        return services;
    }

    public static void ApplyApplicationModuleMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.Database.Migrate();
    }
}
