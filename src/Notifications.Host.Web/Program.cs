using ApplicationRegistry.Infrastructure;
using EventProcessor.Infrastructure;
using Push.Routing;
using Push.Service.Infrastructure;
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

app.Run();
