using Microsoft.AspNetCore.Builder;

namespace WebHooks.Routing;

public static class WebHooksManagerRoutingInstaller
{
    public static IApplicationBuilder UseWebHooksManagerEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
