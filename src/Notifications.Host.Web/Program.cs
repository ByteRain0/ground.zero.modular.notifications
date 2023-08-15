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
    var message = new Message(
        header: new Header("APPCODE", "SOME_TENANT"),
        body: new EventReceived {Payload = "An integration event has been received"});

    var routingKey = RoutingKeys
        .AppEventsTopic
        .ReplaceAppCodePlaceholderWith("NOTIFICATIONS")
        .ReplaceTenantCodePlaceholderWith("ONE");

    messageSender.PublishMessage(message, routingKey);
});

app.Run();
