namespace WebHooks.ResourceAccessor.Contracts;

public interface IWebHooksAccessor
{
    Task<bool> SaveAsync(WebHook webHook);

    Task<bool> DeleteAsync(string id);

    Task<List<WebHook>> GetListAsync(string id);
}
