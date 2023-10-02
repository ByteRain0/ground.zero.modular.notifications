using ApplicationRegistry.Infrastructure;
using EventProcessor.Infrastructure;
using Hangfire;
using Push.Routing;
using Push.Service.Infrastructure;
using Push.Service.Outbox;
using Shared.Cache.Distributed;
using Shared.Cache.Output;
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
    .AddConfiguredOutputCache()
    .AddRabbitMQ(builder.Configuration)
    .AddTokenAccessor()
    .AddSwagger()
    .AddWebHooksService()
    .AddWebHooksRepository()
    .AddApplicationRegistryService(builder.Configuration)
    .AddPushService(builder.Configuration)
    .AddHostedService<WorkerService>()
    .AddEventsProcessor();

var app = builder.Build();

// Set up custom setting for your modules
app
    .ApplyApplicationModuleMigrations()
    .ApplyWebHooksMigrations()
    .ApplyOutboxMigrations();

// Since middleware is sequential if you are using cors use it before response caching.
app.UseOutputCache();

// Register available endpoints
app
    .UseWebHooksServiceEndpoints()
    .UsePushServiceEndpoints()
    .UseAppRegistryEndpoints()
    .UseSwaggerRoutes()
    .UseHangfireDashboard();
app.Run();
