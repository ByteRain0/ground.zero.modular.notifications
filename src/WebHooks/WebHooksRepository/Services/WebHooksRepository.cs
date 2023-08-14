using WebHooks.WebHooksRepository.Contracts;

namespace WebHooks.WebHooksRepository.Services;

public class WebHooksRepository : IWebHooksRepository
{
    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<WebHook>> GetListAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<WebHook>()
        {
            new ()
            {
                Endpoint = new Uri("https://example.com")
            }
        });
    }

    public Task<bool> SaveAsync(WebHook webHook)
    {
        throw new NotImplementedException();
    }
}
