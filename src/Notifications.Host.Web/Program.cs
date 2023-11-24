using ApplicationRegistry.Infrastructure;
using EventProcessor;
using EventProcessor.Infrastructure;
using Hangfire;
using Microsoft.FeatureManagement;
using Push;
using Push.Routing;
using Push.Service.Infrastructure;
using Serilog;
using Shared.Background;
using Shared.Cache.Distributed;
using Shared.Cache.Output;
using Shared.ErrorHandling;
using Shared.HealthChecks;
using Shared.Logging;
using Shared.Messaging;
using Shared.Swagger;
using Shared.TokenService;
using WebHooks;
using WebHooks.WebHooksRepository.Services.Infrastructure;
using WebHooks.WebHooksService.Routing;
using WebHooks.WebHooksService.Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

// Register components
builder.Services
    .AddSerilogLogging(builder.Configuration)
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
    .AddWebHooksRepository(builder.Configuration)
    .AddApplicationRegistryService(builder.Configuration)
    .AddPushService(builder.Configuration)
    .AddEventsProcessor()
    .AddFeatureManagement();

builder.Host.UseSerilog();

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
            url: $"/swagger/{description.GroupName}/swagger.json",
            name: description.GroupName);
    }

    opts.RoutePrefix = string.Empty;
});

app.UseHealthcheckPaths();

app.Run();
