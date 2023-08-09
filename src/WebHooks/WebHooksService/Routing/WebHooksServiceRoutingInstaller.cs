using Microsoft.AspNetCore.Builder;

namespace WebHooks.WebHooksService.Routing;

public static class WebHooksServiceRoutingInstaller
{
    public static IApplicationBuilder UseWebHooksServiceEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
