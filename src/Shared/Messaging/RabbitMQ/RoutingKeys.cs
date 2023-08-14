namespace Shared.Messaging.RabbitMQ;

public static class RoutingKeys
{
    private const string EventsTopic = "events.[tenantcode]";

    public const string AppEventsTopic = $"{EventsTopic}.[appCode]";

    public const string WebHooksTopic = $"{AppEventsTopic}.webhooks";

    public const string IosNotificationsTopic = $"{AppEventsTopic}.mobile.ios";

    public const string AndroidNotificationsTopic = $"{AppEventsTopic}.mobile.android";

    public static string ReplaceTenantCodePlaceholderWith(this string routingKey, string tenantCode)
        => routingKey.Replace("[tenantcode]", tenantCode.ToLowerInvariant());

    public static string ReplaceAppCodePlaceholderWith(this string routingKey, string appName)
        => routingKey.Replace("[appCode]", appName.ToLowerInvariant());
}
