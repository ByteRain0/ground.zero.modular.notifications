using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.PostgreSql.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Background;

public static class BackgroundJobsInstaller
{
    public static IServiceCollection AddBackgroundJobs(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        var connectionFactory = new NpgsqlConnectionFactory(connectionString, new PostgreSqlStorageOptions());

        services.AddHangfire(hangfireConfiguration =>
        {
            hangfireConfiguration.UsePostgreSqlStorage(config =>
            {
                config.UseNpgsqlConnection(connectionString);
                config.UseConnectionFactory(connectionFactory);
            });
        });

        JobStorage.Current = new PostgreSqlStorage(connectionFactory);

        services.AddHangfireServer();

        return services;
    }
}
