using Microsoft.AspNetCore.Routing;

namespace Shared.Routing;

public interface IEndpointsDefinition
{
    public static abstract void ConfigureEndpoints(IEndpointRouteBuilder app);
}
