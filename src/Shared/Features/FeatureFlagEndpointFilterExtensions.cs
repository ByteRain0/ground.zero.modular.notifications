using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Shared.Features;

public static class FeatureFlagEndpointFilterExtensions
{
    public static TBuilder WithFeatureFlag<TBuilder>(
            this TBuilder builder,
            string endpointName)
            where TBuilder : IEndpointConventionBuilder
        => builder.AddEndpointFilter(new FeatureFlagEndpointFilter(endpointName));
}
