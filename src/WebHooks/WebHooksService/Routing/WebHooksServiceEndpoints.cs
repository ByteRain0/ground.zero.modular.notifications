using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using O9d.AspNet.FluentValidation;
using Shared.Pagination;
using Shared.Routing;
using Shared.Sorting;
using WebHooks.WebHooksService.Contracts.Commands.CreateWebHook;
using WebHooks.WebHooksService.Contracts.Commands.DeleteWebHook;
using WebHooks.WebHooksService.Contracts.Models;
using WebHooks.WebHooksService.Contracts.Queries.GetWebHook;
using WebHooks.WebHooksService.Contracts.Queries.GetWebHooks;

namespace WebHooks.WebHooksService.Routing;

public class WebHooksServiceEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/webHooks")
            .WithTags("WebHooks")
            .WithOpenApi()
            .WithValidationFilter();

        group.MapPost("/", CreateWebHook)
            .WithName("CreateWebHook")
            .Accepts<CreateWebHookCommand>(ContentTypes.ApplicationJson)
            .Produces(200)
            .ProducesValidationProblem();

        group.MapDelete("/{id:int}", DeleteWebHook)
            .WithName("DeleteWebHook")
            .Accepts<DeleteWebHookCommand>(ContentTypes.ApplicationJson)
            .Produces(204)
            .Produces(404)
            .ProducesValidationProblem();

        group.MapGet("/", GetWebHooks)
            .WithName("GetWebHooks")
            .Produces<PagedList<WebHookDto>>(200)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> GetWebHooks(
        string sourceCode,
        string eventCode,
        string tennantCode,
        int? page,
        int? pageSize,
        string? sortColumn,
        SortOrder? sortOrder,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetWebHooksQuery
        {
            SourceCode = sourceCode,
            EventCode = eventCode,
            TennantCode = tennantCode,
            Page = page ?? 0,
            PageSize = pageSize ?? 10,
            SortColumn = sortColumn,
            SortOrder = sortOrder
        };

        return Results.Ok(await mediator.Send(query, cancellationToken));
    }

    private static async Task<IResult> CreateWebHook(
        [Validate][FromBody]CreateWebHookCommand command,
        [FromServices] IMediator mediator)
    {
        await mediator.Send(command);
        return Results.Ok();
    }

    private static async Task<IResult> DeleteWebHook(
        [Validate] [FromBody] DeleteWebHookCommand command,
        [FromServices] IMediator mediator)
    {
        var webHook = await mediator.Send(new GetWebHookQuery() { Id = command.Id });

        if (webHook == null)
        {
            return Results.NotFound();
        }

        await mediator.Send(command);

        return Results.NoContent();
    }
}
