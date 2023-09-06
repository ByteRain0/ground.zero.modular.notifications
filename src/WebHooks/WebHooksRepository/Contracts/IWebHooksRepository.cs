using Shared.Pagination;

namespace WebHooks.WebHooksRepository.Contracts;

/// <summary>
/// TODO: this can be made as a generic repository as well as for the moment we don't have anything specific in here.
/// However I don't really like this approach.
/// </summary>
public interface IWebHooksRepository
{
    Task<bool> SaveAsync(WebHook webHook);

    Task<bool> DeleteAsync(string id);

    Task<WebHook> GetAsync(string id, CancellationToken cancellationToken);

    Task<PagedList<WebHook>> GetListAsync(GetListAsyncQuery query, CancellationToken cancellationToken);
}
