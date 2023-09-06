using Microsoft.AspNetCore.Builder;
using Shared.Routing;

namespace WebHooks.WebHooksService.Routing;

public static class WebHooksServiceRoutingInstaller
{
    public static IApplicationBuilder UseWebHooksServiceEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints<WebHooksServiceEndpoints>();
        return app;
    }
}
