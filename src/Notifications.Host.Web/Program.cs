using ApplicationRegistry.Infrastructure;
using Push.Infrastructure;
using WebHooks.Manager.Infrastructure;
using WebHooks.ResourceAccessor.Infrastructure;
using WebHooks.Routing;

var builder = WebApplication.CreateBuilder(args);

// Register modules
builder.Services
    .AddWebHooksManager()
    .AddWebHooksAccessor()
    .AddApplicationRegistryAccessor()
    .AddPushManager();

var app = builder.Build();

// Register available endpoints
app
    .UseWebHooksManagerEndpoints()
    .UsePushManagerEndpoints();

app.Run();
