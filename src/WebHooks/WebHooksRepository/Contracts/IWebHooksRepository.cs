using Shared.Pagination;

namespace WebHooks.WebHooksRepository.Contracts;

public interface IWebHooksRepository
{
    Task<bool> SaveAsync(WebHook webHook);

    Task<bool> DeleteAsync(string id);

    Task<WebHook?> GetById(string id);

    Task<WebHook> GetAsync(string id, CancellationToken cancellationToken);

    Task<PagedList<WebHook>> GetListAsync(GetListAsyncQuery query, CancellationToken cancellationToken);
}
