using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Routing;

namespace Shared.ErrorHandling;

/// <summary>
/// First way to use exception handling
/// </summary>
public class ErrorHandlingEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.Map("/error", HandleError);

        app.Map("/exception", ()
            => { throw new InvalidOperationException("Sample Exception"); });
    }

    private static IResult HandleError(HttpContext httpContext)
    {
        var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is not null)
        {
            return Results.Problem(exception.Message);
        }

        return Results.Problem();
    }
}
