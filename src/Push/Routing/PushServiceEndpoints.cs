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
            .Build();

        app.MapPost("api/v{version:apiVersion}/events", ProcessIncomingEvent)
            .WithName("ProcessEvent_v1")
            .ProducesValidationProblem()
            .Produces(200)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0);
    }

    private static async Task<IResult> ProcessIncomingEvent(
        [Validate][FromBody] IncomingEvent @event,
        [FromServices] PushService pushService)
    {
        await pushService.ProcessIncomingEventAsync(@event);
        return Results.Ok();
    }
}
