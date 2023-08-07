namespace WebHooks.Manager.Contracts.Queries;

public record GetWebHooksQuery
{
    public string SourceCode { get; set; }

    public string EventCode { get; set; }

    public string TennantCode { get; set; }
}
