using ApplicationRegistry.Infrastructure;
using EventProcessor.Infrastructure;
using Push.Routing;
using Push.Service.Infrastructure;
using Shared.Messaging.IntegrationEvents;
using Shared.Messaging.RabbitMQ;
using WebHooks.WebHooksRepository.Services.Infrastructure;
using WebHooks.WebHooksService.Routing;
using WebHooks.WebHooksService.Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register modules

builder.Services
    .AddWebHooksService()
    .AddWebHooksRepository()
    .AddApplicationRegistryService()
    .AddPushService()
    .AddHostedService<WorkerService>()
    .AddEventsProcessor()
    .AddRabbitMQ(builder.Configuration);

var app = builder.Build();

// Register available endpoints
app
    .UseWebHooksServiceEndpoints()
    .UsePushServiceEndpoints();

app.MapGet("/", (IMessageSender messageSender) =>
{
    var routingKey = RoutingKeys
        .AppEventsTopic
        .ReplaceAppCodePlaceholderWith("NOTIFICATIONS")
        .ReplaceTenantCodePlaceholderWith("ONE");

    messageSender.PublishMessage(
        new Message
        {
            Header = new Header {AppCode = "NOTIFICATIONS", TenantCode = "ONE", DateTime = DateTimeOffset.UtcNow},
            Body = "This is a message body"
        }, routingKey
    );
});

app.Run();
