using ApplicationRegistry.Infrastructure;
using EventProcessor.Infrastructure;
using Push.Routing;
using Push.Service.Infrastructure;
using Shared.Cache;
using Shared.Messaging.RabbitMQ;
using Shared.TokenService;
using WebHooks.WebHooksRepository.Services.Infrastructure;
using WebHooks.WebHooksService.Routing;
using WebHooks.WebHooksService.Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register components
builder.Services
    .AddWebHooksService()
    .AddWebHooksRepository()
    .AddApplicationRegistryService(builder.Configuration)
    .AddPushService()
    .AddHostedService<WorkerService>()
    .AddEventsProcessor()
    .AddRabbitMQ(builder.Configuration)
    .AddTokenAccessor()
    .AddCacheService(builder.Configuration);

var app = builder.Build();

// Set up custom setting for your modules
app.ApplyApplicationModuleMigrations();

// Register available endpoints
app
    .UseWebHooksServiceEndpoints()
    .UsePushServiceEndpoints();

app.Run();
