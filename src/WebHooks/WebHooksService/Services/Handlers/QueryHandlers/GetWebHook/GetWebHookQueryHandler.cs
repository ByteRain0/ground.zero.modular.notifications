using FluentResults;
using MediatR;
using Shared.Result;
using WebHooks.Contracts.Models;
using WebHooks.Contracts.Queries.GetWebHook;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Services.Mappings;
using ErrorCodes = WebHooks.Contracts.ErrorCodes;

namespace WebHooks.WebHooksService.Services.Handlers.QueryHandlers.GetWebHook;

public class GetWebHookQueryHandler : IRequestHandler<GetWebHookQuery, Result<WebHookDto>>
{
    private readonly IWebHooksRepository _webHooksRepository;

    public GetWebHookQueryHandler(IWebHooksRepository webHooksRepository)
    {
        _webHooksRepository = webHooksRepository;
    }

    public async Task<Result<WebHookDto>> Handle(GetWebHookQuery request, CancellationToken cancellationToken)
    {
        var retrievalOperation = await _webHooksRepository.GetById(request.Id.ToString());

        if (retrievalOperation.IsFailed && retrievalOperation.IsNotFoundError())
        {
            return Result.Fail(ErrorCodes.WebHookNotFound);
        }

        if (retrievalOperation.IsFailed)
        {
            return Result.Fail(ErrorCodes.GeneralModuleIssues);
        }

        return retrievalOperation.Value.ToDto();
    }
}
