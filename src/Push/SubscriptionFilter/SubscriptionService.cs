namespace Push.SubscriptionFilter;

public class SubscriptionService : ISubscriptionService
{
    public Task<bool> HasCreditsAsync(string apiKey)
    {
        if (apiKey == "test_key")
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}
