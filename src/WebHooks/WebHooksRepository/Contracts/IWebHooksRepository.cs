using FluentResults;
using Shared.Pagination;

namespace WebHooks.WebHooksRepository.Contracts;

public interface IWebHooksRepository
{
    Task<Result> SaveAsync(WebHook webHook);

    Task<Result> DeleteAsync(string id);

    Task<Result<WebHook>> GetById(string id);

    Task<Result<WebHook>> GetAsync(string id, CancellationToken cancellationToken);

    Task<Result<PagedList<WebHook>>> GetListAsync(GetListAsyncQuery query, CancellationToken cancellationToken);
}
