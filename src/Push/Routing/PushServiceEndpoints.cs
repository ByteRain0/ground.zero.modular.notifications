using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using O9d.AspNet.FluentValidation;
using Push.Contracts.Contract;
using Push.Service;
using Shared.Routing;

namespace Push.Routing;

internal class PushServiceEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .HasApiVersion(new ApiVersion(2.0))
            .ReportApiVersions()
            .Build();

        app.MapPost("api/v{version:apiVersion}/events", ProcessIncomingEvent)
            .WithName("ProcessEvent_v1")
            .ProducesValidationProblem()
            .Produces(200)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0);

        //Essentially you would create a same class structure for v2 if you want to have 2 versions at the same time
        app.MapPost("api/v{version:apiVersion}/events", ProcessIncomingEvent)
            .WithName("ProcessEvent_v2")
            .ProducesValidationProblem()
            .Produces(200)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(2.0);
    }

    private static async Task<IResult> ProcessIncomingEvent(
        [Validate][FromBody] IncomingEvent @event,
        [FromServices] PushService pushService,
        HttpContext context)
    {
        var apiVersion = context.GetRequestedApiVersion();
        Console.WriteLine(apiVersion);
        await pushService.ProcessIncomingEventAsync(@event);
        return Results.Ok();
    }
}
