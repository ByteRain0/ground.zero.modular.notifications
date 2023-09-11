using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Messaging.IntegrationEvents;
using Shared.Messaging.RabbitMQ;
using Shared.Routing;

namespace Push.Routing;

internal class PushServiceEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/test", (IMessageSender messageSender) =>
        {
            var message = new Message(
                header: new Header("APPCODE", "SOME_TENANT", "DEFAULT_EVENT"),
                body: new EventReceived {Payload = "An integration event has been received"});

            var routingKey = RoutingKeys
                .AppEventsTopic
                .ReplaceAppCodePlaceholderWith("APPCODE")
                .ReplaceTenantCodePlaceholderWith("SOME_TENANT");

            messageSender.PublishMessage(message, routingKey);
        })
            .WithTags("PushEvents")
            .WithName("TestIntegrationEvent");
    }
}
