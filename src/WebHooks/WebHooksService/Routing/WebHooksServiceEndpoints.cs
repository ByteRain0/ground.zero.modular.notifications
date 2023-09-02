using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using O9d.AspNet.FluentValidation;
using Shared.Routing;
using WebHooks.WebHooksService.Contracts.Queries;

namespace WebHooks.WebHooksService.Routing;

public class WebHooksServiceEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/webhooks")
            .WithTags("WebHooks")
            .WithOpenApi()
            .WithValidationFilter();
    }

    public static async Task<IResult> GetWebHooks(
        [Validate] GetWebHooksQuery query,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(query, cancellationToken));
    }
}
