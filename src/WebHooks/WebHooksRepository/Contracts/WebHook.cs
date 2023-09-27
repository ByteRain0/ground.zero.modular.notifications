namespace WebHooks.WebHooksRepository.Contracts;

public class WebHook
{
    public string Id { get; set; }

    public Uri Endpoint { get; set; }

    public string ClientCode { get; set; }

    public string SourceCode { get; set; }

    public string EventCode { get; set; }

    public string TenantCode { get; set; }
}
