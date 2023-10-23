using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Shared.ErrorHandling;

public class SimplifiedErrorHandlingEndpoints //: IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.Map("/exception", ()
            => { throw new InvalidOperationException("Sample Exception"); });
    }
}
