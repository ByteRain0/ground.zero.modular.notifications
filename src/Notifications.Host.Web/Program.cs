using ApplicationRegistry.Services.Infrastructure;
using Push.Routing;
using Push.Service.Infrastructure;
using WebHooks.WebHooksRepository.Services.Infrastructure;
using WebHooks.WebHooksService.Routing;
using WebHooks.WebHooksService.Services.Infrastructure;

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
