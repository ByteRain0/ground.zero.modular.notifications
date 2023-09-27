using MediatR;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Contracts.Models;
using WebHooks.WebHooksService.Contracts.Queries.GetWebHook;
using WebHooks.WebHooksService.Services.Mappings;

namespace WebHooks.WebHooksService.Services.Handlers.QueryHandlers.GetWebHook;

public class GetWebHookQueryHandler : IRequestHandler<GetWebHookQuery, WebHookDto?>
{
    private readonly IWebHooksRepository _webHooksRepository;

    public GetWebHookQueryHandler(IWebHooksRepository webHooksRepository)
    {
        _webHooksRepository = webHooksRepository;
    }

    public async Task<WebHookDto?> Handle(GetWebHookQuery request, CancellationToken cancellationToken)
    {
        var webHook = await _webHooksRepository.GetById(request.Id.ToString());

        if (webHook == null)
        {
            return default;
        }

        return webHook.ToDto();
    }
}
