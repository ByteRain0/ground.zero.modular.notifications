using ApplicationRegistry.Infrastructure;
using EventProcessor.Infrastructure;
using Push.Routing;
using Push.Service.Infrastructure;
using Shared.Cache;
using Shared.Messaging.RabbitMQ;
using Shared.Swagger;
using Shared.TokenService;
using WebHooks.WebHooksRepository.Services.Infrastructure;
using WebHooks.WebHooksService.Routing;
using WebHooks.WebHooksService.Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register components
builder.Services
    .AddCacheService(builder.Configuration)
    .AddRabbitMQ(builder.Configuration)
    .AddTokenAccessor()
    .AddSwagger()
    .AddWebHooksService()
    .AddWebHooksRepository()
    .AddApplicationRegistryService(builder.Configuration)
    .AddPushService()
    .AddHostedService<WorkerService>()
    .AddEventsProcessor();

var app = builder.Build();

// Set up custom setting for your modules
app.ApplyApplicationModuleMigrations();

// Register available endpoints
app
    .UseWebHooksServiceEndpoints()
    .UsePushServiceEndpoints()
    .UseAppRegistryEndpoints()
    .UseSwaggerRoutes();

app.Run();
