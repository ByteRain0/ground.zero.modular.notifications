using Microsoft.AspNetCore.Builder;
using Shared.Routing;

namespace Push.Routing;

public static class PushServiceRoutingInstaller
{
    public static IApplicationBuilder UsePushServiceEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints<PushServiceEndpoints>();
        return app;
    }
}
