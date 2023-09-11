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
using WebHooks.WebHooksService.Contracts.Models;
using WebHooks.WebHooksService.Contracts.Queries;

namespace WebHooks.WebHooksService.Routing;

public class WebHooksServiceEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/webhooks")
            .WithTags("WebHooks")
            .WithOpenApi()
            .WithValidationFilter();

        group.MapPost("/", CreateWebHook)
            .WithName("CreateWebHook")
            .Accepts<CreateWebHookCommand>(ContentTypes.ApplicationJson)
            .Produces(200)
            .ProducesValidationProblem();

        group.MapGet("/", GetWebHooks)
            .WithName("GetWebHooks")
            .Produces<PagedList<WebHookDto>>(200)
            .ProducesValidationProblem();
    }

    public static async Task<IResult> GetWebHooks(
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

    public static async Task<IResult> CreateWebHook(
        [Validate][FromBody]CreateWebHookCommand command,
        [FromServices] IMediator mediator)
    {
        await mediator.Send(command);
        return Results.Ok();
    }
}
