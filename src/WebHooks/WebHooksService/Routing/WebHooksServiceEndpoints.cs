using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using O9d.AspNet.FluentValidation;
using Shared.Routing;
using WebHooks.WebHooksService.Contracts.Commands.CreateWebHook;
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

        app.MapPost("/", CreateWebHook)
            .WithName("CreateWebHook")
            .Accepts<CreateWebHookCommand>(ContentTypes.ApplicationJson)
            .Produces(200)
            .ProducesValidationProblem();
    }

    public static async Task<IResult> GetWebHooks(
        [Validate] GetWebHooksQuery query,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(query, cancellationToken));
    }

    public static async Task<IResult> CreateWebHook(
        [Validate][FromBody]CreateWebHookCommand command,
        [FromServices] IMediator mediator)
    {
        await mediator.Send(command);
        return Results.Ok();
    }
}
