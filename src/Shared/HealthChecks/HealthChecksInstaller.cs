using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Shared.HealthChecks;

public static class HealthChecksInstaller
{
    public static WebApplication UseHealthcheckPaths(this WebApplication app)
    {
        app.MapHealthChecks("healthz/ready",
            new HealthCheckOptions() {Predicate = health => health.Tags.Contains("ready")});

        app.MapHealthChecks("healthz/live",
            new HealthCheckOptions() {ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse});

        return app;
    }
}

public class HealthConstants
{
    public static List<string> ReadinessTags = new() {"ready"};
}
