using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using O9d.AspNet.FluentValidation;
using Shared.Pagination;
using Shared.Result;
using Shared.Routing;
using Shared.Sorting;
using WebHooks.Contracts;
using WebHooks.Contracts.Commands.CreateWebHook;
using WebHooks.Contracts.Commands.DeleteWebHook;
using WebHooks.Contracts.Models;
using WebHooks.Contracts.Queries.GetWebHook;
using WebHooks.Contracts.Queries.GetWebHooks;

namespace WebHooks.WebHooksService.Routing;

public class WebHooksServiceEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("api/v{version:apiVersion}/webHooks")
            .WithTags("WebHooks")
            .WithOpenApi()
            .WithValidationFilter()
            .WithApiVersionSet(versionSet);

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
        [FromServices] IMediator mediator,
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

        var operationResult = await mediator.Send(query, cancellationToken);

        if (operationResult.IsFailed)
        {
            return Results.Problem(ErrorCodes.DataRetrievalIssues);
        }

        return Results.Ok(operationResult.Value);
    }

    private static async Task<IResult> CreateWebHook(
        [Validate] [FromBody] CreateWebHookCommand command,
        [FromServices] IMediator mediator)
    {
        await mediator.Send(command);
        return Results.Ok();
    }

    private static async Task<IResult> DeleteWebHook(
        [Validate] [FromBody] DeleteWebHookCommand command,
        [FromServices] IMediator mediator)
    {
        var webHook = await mediator.Send(new GetWebHookQuery() {Id = command.Id});

        if (webHook.IsFailed && webHook.IsNotFoundError())
        {
            return Results.NotFound();
        }

        if (webHook.IsFailed)
        {
            return Results.Problem(ErrorCodes.GeneralModuleIssues);
        }

        var operation = await mediator.Send(command);

        if (operation.IsFailed)
        {
            return Results.Problem(ErrorCodes.DataMutationFailure);
        }

        return Results.NoContent();
    }
}
