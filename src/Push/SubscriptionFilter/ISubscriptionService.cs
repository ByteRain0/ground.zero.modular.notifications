namespace Push.SubscriptionFilter;

public interface ISubscriptionService
{
    Task<bool> HasCreditsAsync(string apiKey);
}
