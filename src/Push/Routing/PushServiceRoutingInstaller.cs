using Microsoft.AspNetCore.Builder;

namespace Push.Routing;

public static class PushServiceRoutingInstaller
{
    public static IApplicationBuilder UsePushServiceEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
