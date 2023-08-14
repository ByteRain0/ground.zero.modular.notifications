using MediatR;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Contracts.Models;
using WebHooks.WebHooksService.Contracts.Queries;

namespace WebHooks.WebHooksService.Services.Handlers.QueryHandlers;

internal class GetWebHooksQueryHandler : IRequestHandler<GetWebHooksQuery, List<WebHookDto>>
{
    private readonly IWebHooksRepository _webHooksRepository;

    public GetWebHooksQueryHandler(IWebHooksRepository webHooksRepository)
    {
        _webHooksRepository = webHooksRepository;
    }

    public async Task<List<WebHookDto>> Handle(GetWebHooksQuery request, CancellationToken cancellationToken)
    {
        //TODO: pass in a specification here for filtering.
        var webHooksList = await _webHooksRepository.GetListAsync(cancellationToken);
        //TODO: return a list with remapping it to the DTOs
        return webHooksList.Select(_ => new WebHookDto()).ToList();
    }
}
