namespace WebHooks.WebHooksRepository.Contracts;

public record GetListAsyncQuery
{
    public string EventCode { get; init; }
}
