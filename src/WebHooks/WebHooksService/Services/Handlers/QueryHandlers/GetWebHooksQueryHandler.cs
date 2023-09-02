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
        var webHooksList = await _webHooksRepository.GetListAsync(new GetListAsyncQuery(),cancellationToken);
        return webHooksList.Select(_ => new WebHookDto()).ToList();
    }
}
