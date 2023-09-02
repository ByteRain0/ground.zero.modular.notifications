namespace WebHooks.WebHooksRepository.Contracts;

public interface IWebHooksRepository
{
    Task<bool> SaveAsync(WebHook webHook);

    Task<bool> DeleteAsync(string id);

    Task<List<WebHook>> GetListAsync(GetListAsyncQuery query, CancellationToken cancellationToken);
}
