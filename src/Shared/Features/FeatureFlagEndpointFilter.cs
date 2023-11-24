using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Shared.Features;

public class FeatureFlagEndpointFilter : IEndpointFilter
{
    private readonly string _endpointName;

    public FeatureFlagEndpointFilter(string endpointName)
    {
        _endpointName = endpointName;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var featureManager = context.HttpContext.RequestServices.GetRequiredService<IFeatureManager>();

        var isEnabled = await featureManager.IsEnabledAsync($"api_{_endpointName}");

        if (!isEnabled)
        {
            return Results.NotFound();
        }

        return await next(context);
    }
}
