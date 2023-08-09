using ApplicationRegistry.Infrastructure;
using Push.Infrastructure;
using Push.Routing;
using WebHooks.WebHooksRepository.Infrastructure;
using WebHooks.WebHooksService.Infrastructure;
using WebHooks.WebHooksService.Routing;

var builder = WebApplication.CreateBuilder(args);

// Register modules
builder.Services
    .AddWebHooksService()
    .AddWebHooksRepository()
    .AddApplicationRegistryService()
    .AddPushService();

var app = builder.Build();

// Register available endpoints
app
    .UseWebHooksServiceEndpoints()
    .UsePushServiceEndpoints();

app.Run();
