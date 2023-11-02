using ApplicationRegistry.Infrastructure;
using EventProcessor;
using EventProcessor.Infrastructure;
using Hangfire;
using Push;
using Push.Routing;
using Push.Service.Infrastructure;
using Shared.Background;
using Shared.Cache.Distributed;
using Shared.Cache.Output;
using Shared.ErrorHandling;
using Shared.Messaging;
using Shared.Swagger;
using Shared.TokenService;
using WebHooks;
using WebHooks.WebHooksRepository.Services.Infrastructure;
using WebHooks.WebHooksService.Routing;
using WebHooks.WebHooksService.Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register components
builder.Services
    .AddBackgroundJobs(builder.Configuration)
    .AddGlobalErrorHandling()
    .AddCacheService(builder.Configuration)
    .AddConfiguredOutputCache()
    .AddAsyncProcessing(builder.Configuration,
        assembliesWithConsumers: new[]
        {
            typeof(IPushServiceMarker).Assembly, typeof(IWebHooksServiceMarker).Assembly,
            typeof(IEventProcessorMarker).Assembly
        })
    .AddTokenAccessor()
    .AddSwagger()
    .AddWebHooksService()
    .AddWebHooksRepository()
    .AddApplicationRegistryService(builder.Configuration)
    .AddPushService(builder.Configuration)
    .AddEventsProcessor();

var app = builder.Build();

// Set up custom setting for your modules
app
    .ApplyApplicationModuleMigrations()
    .ApplyWebHooksMigrations()
    .ApplyOutboxMigrations()
    ;

// Register Middleware components separately for readability
app
// Since middleware is sequential if you are using cors use it before response caching.
    .UseOutputCache()
    .UseErrorHandling()
    ;

// Register available endpoints
app
    .UseWebHooksServiceEndpoints()
    .UsePushServiceEndpoints()
    .UseAppRegistryEndpoints()
    .UseHangfireDashboard()
    ;

app.UseSwagger();
app.UseSwaggerUI(opts =>
{
    foreach (var description in app.DescribeApiVersions())
    {
        opts.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName);
    }
    opts.RoutePrefix = string.Empty;
});

app.Run();
