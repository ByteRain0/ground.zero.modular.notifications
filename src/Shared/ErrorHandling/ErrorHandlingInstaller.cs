using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Routing;

namespace Shared.ErrorHandling;

public static class ErrorHandlingInstaller
{
    public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
    {
        services.AddProblemDetails();
        return services;
    }

    public static IApplicationBuilder UseCustomGlobalErrorHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler("/error");
        app.UseStatusCodePages();
        app.UseEndpoints<ErrorHandlingEndpoints>();
        return app;
    }

    public static IApplicationBuilder UseBuiltInErrorHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        app.UseStatusCodePages();
        app.UseEndpoints<SimplifiedErrorHandlingEndpoints>();
        return app;
    }
}
