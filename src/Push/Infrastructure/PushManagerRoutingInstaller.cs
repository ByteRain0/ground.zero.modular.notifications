using Microsoft.AspNetCore.Builder;

namespace Push.Infrastructure;

public static class PushManagerRoutingInstaller
{
    public static IApplicationBuilder UsePushManagerEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
