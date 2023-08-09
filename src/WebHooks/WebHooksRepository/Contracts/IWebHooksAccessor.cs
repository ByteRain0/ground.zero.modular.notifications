namespace WebHooks.WebHooksRepository.Contracts;

public interface IWebHooksAccessor
{
    Task<bool> SaveAsync(WebHook webHook);

    Task<bool> DeleteAsync(string id);

    Task<List<WebHook>> GetListAsync(CancellationToken cancellationToken);
}
