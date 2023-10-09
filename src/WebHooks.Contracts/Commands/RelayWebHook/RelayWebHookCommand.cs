namespace WebHooks.Contracts.Commands.RelayWebHook;

public class RelayWebHookCommand
{
    public string TenantCode { get; set; }

    public string EventCode { get; set; }

    public string SourceCode { get; set; }

    public string Payload { get; set; }

    public Uri Endpoint { get; set; }
}
