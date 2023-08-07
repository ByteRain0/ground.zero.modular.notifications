namespace WebHooks.ResourceAccessor.Contracts;

/// <summary>
/// Public contract of the WebHooksAccessor Service.
/// </summary>
public class WebHook
{
    public Guid Id { get; set; }

    public Uri Endpoint { get; set; }

    public string ClientCode { get; set; }

    public string SourceCode { get; set; }

    public string EventCode { get; set; }

    public string TenantCode { get; set; }
}
