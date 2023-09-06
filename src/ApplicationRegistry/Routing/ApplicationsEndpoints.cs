using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shared.Pagination;
using Shared.Routing;
using Shared.Sorting;

namespace ApplicationRegistry.Routing;

public class ApplicationsEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/applications")
            .WithTags("Applications")
            .WithOpenApi();

        group.MapGet("/", GetApplications)
            .Produces<PagedList<Application>>(200)
            .ProducesValidationProblem()
            .WithName("GetApplications")
            .CacheOutput("GetApplications");

        group.MapGet("/{code}", GetApplicationByCode)
            .Accepts<GetApplicationListQuery>(ContentTypes.ApplicationJson)
            .Produces<Application>(200)
            .Produces(404)
            .ProducesValidationProblem()
            .WithName("GetApplicationByCode");

        group.MapGet("/invalidate", InvalidateMoviesCache)
            .WithName("InvalidateCache");
    }

    public static async Task<IResult> GetApplicationByCode(
        string code,
        IApplicationsRepository repository,
        CancellationToken cancellationToken)
    {
        var application = await repository.GetByCodeAsync(code, cancellationToken);

        if (application is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(application);
    }

    public static async Task<IResult> GetApplications(
        [FromQuery] string? sortColumn,
        [FromQuery] SortOrder? sortOrder,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        IApplicationsRepository repository,
        CancellationToken cancellationToken)
    {
        var query = new GetApplicationListQuery
        {
            SortColumn = sortColumn, SortOrder = sortOrder, Page = page, PageSize = pageSize
        };
        var applications = await repository.GetListAsync(query, cancellationToken);
        return Results.Ok(applications);
    }

    public static async Task<IResult> InvalidateMoviesCache(
        IOutputCacheStore cacheStore,
        CancellationToken cancellationToken)
    {
        await cacheStore.EvictByTagAsync("movies", cancellationToken);

        return Results.Ok();
    }
}
