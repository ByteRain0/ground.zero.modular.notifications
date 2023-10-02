using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.PostgreSql.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Push.Service.Outbox;

public static class OutboxInstaller
{
    public static IServiceCollection AddRabbitMQOutbox(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")!;

        services.AddDbContext<OutboxDbContext>(opts =>
            opts.UseNpgsql(connectionString));

        services.AddScoped<OutboxService>();

        var connectionFactory = new NpgsqlConnectionFactory(connectionString, new PostgreSqlStorageOptions());
        services.AddHangfire(hangFireConfiguration =>
        {
            hangFireConfiguration.UsePostgreSqlStorage(config =>
            {
                config.UseNpgsqlConnection(connectionString);
                config.UseConnectionFactory(connectionFactory);
            });
        });
        JobStorage.Current = new PostgreSqlStorage(connectionFactory);
        RecurringJob.AddOrUpdate("OutboxRelay",
            () => services.BuildServiceProvider().GetService<OutboxService>()!.ProcessExistingMessagesAsync(), Cron.Minutely);

        services.AddHangfireServer();

        return services;
    }

    public static IApplicationBuilder ApplyOutboxMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<OutboxDbContext>();
        context?.Database.Migrate();
        return app;
    }
}
