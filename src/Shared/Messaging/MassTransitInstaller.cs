using System.Reflection;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging;

public static class MassTransitInstaller
{
    public static IServiceCollection AddAsyncProcessing(this IServiceCollection services, IConfiguration configuration, Assembly[] assembliesWithConsumers)
    {
        //Since we don't want the config to spill out do not add it as options to DI container.
        var configData = configuration.GetSection("RabbitMQSettings");
        var brokerSettings = new BrokerSettings();
        configData.Bind(brokerSettings);

        // Resolve the DbContext for the OutBox
        services.AddDbContext<RegistrationDbContext>(x =>
        {
            var connectionString = configuration.GetConnectionString("Default");

            x.UseNpgsql(connectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                options.MigrationsHistoryTable($"__{nameof(RegistrationDbContext)}");

                options.EnableRetryOnFailure(3);
                options.MinBatchSize(1);
            });
        });

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddEntityFrameworkOutbox<RegistrationDbContext>(o =>
            {
                // configure which database lock provider to use (Postgres, SqlServer, or MySql)
                o.UsePostgres();

                // enable the bus outbox
                o.UseBusOutbox();

                o.QueryDelay = TimeSpan.FromMinutes(1);
            });

            x.AddConsumers(assembliesWithConsumers);
            x.UsingRabbitMq((context,cfg) =>
            {
                cfg.Host(brokerSettings.Host, "/", h => {
                    h.Username(brokerSettings.Username);
                    h.Password(brokerSettings.Password);
                });
                cfg.AutoStart = true;
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddSingleton<ILockStatementProvider, PostgresLockStatementProvider>();

        services.AddTransient<IMessageSender, MessageSender>();

        return services;
    }

    public static IApplicationBuilder ApplyOutboxMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<RegistrationDbContext>();
        context?.Database.EnsureCreated();
        context?.Database.Migrate();

        return app;
    }
}
