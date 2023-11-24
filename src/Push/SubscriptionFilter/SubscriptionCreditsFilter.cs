using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace Push.SubscriptionFilter;

[FilterAlias("SubscriptionCreditsFilter")]
public class SubscriptionCreditsFilter : IFeatureFilter
{
    private readonly ISubscriptionService _subscriptionService;

    private readonly IHttpContextAccessor _contextAccessor;

    public SubscriptionCreditsFilter(
        ISubscriptionService subscriptionService,
        IHttpContextAccessor contextAccessor)
    {
        _subscriptionService = subscriptionService;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var apiKey = _contextAccessor.HttpContext?.Request.Headers["x-api-key"];

        if (apiKey is null)
        {
            return false;
        }

        var exceptKeys = context.Parameters.Get<SubscriptionCreditsFilterSettings>();

        if (exceptKeys != null && exceptKeys.IgnoreForClients.Any(x => x == apiKey.ToString()))
        {
            return true;
        }

        return await _subscriptionService.HasCreditsAsync(apiKey.ToString()!);
    }
}
