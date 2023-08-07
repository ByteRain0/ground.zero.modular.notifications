namespace Shared;

public static class RoutingKeys
{
    private const string EventsTopic = "events.[tenantcode]";

    public const string AppEventsTopic = $"{EventsTopic}.[appName]";

    public const string WebHooksTopic = $"{AppEventsTopic}.webhooks";

    public const string IosNotificationsTopic = $"{AppEventsTopic}.mobile.ios";

    public const string AndroidNotificationsTopic = $"{AppEventsTopic}.mobile.android";

    public static string ReplaceTenantNamePlaceholderWith(this string routingKey, string tenantCode)
        => routingKey.Replace("[tenantcode]", tenantCode.ToLowerInvariant());

    public static string ReplaceAppNamePlaceholderWith(this string routingKey, string appName)
        => routingKey.Replace("[appName]", appName.ToLowerInvariant());
}
