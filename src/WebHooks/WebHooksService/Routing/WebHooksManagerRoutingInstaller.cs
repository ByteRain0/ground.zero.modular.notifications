using Microsoft.AspNetCore.Builder;

namespace WebHooks.WebHooksService.Routing;

public static class WebHooksManagerRoutingInstaller
{
    public static IApplicationBuilder UseWebHooksServiceEndpoints(this IApplicationBuilder app)
    {
        return app;
    }
}
