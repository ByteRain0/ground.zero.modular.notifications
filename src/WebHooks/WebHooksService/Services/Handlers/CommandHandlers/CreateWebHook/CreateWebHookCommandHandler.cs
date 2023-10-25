using ApplicationRegistry.Contracts;
using FluentResults;
using MediatR;
using WebHooks.Contracts.Commands.CreateWebHook;
using WebHooks.WebHooksRepository.Contracts;
using ErrorCodes = WebHooks.Contracts.ErrorCodes;

namespace WebHooks.WebHooksService.Services.Handlers.CommandHandlers.CreateWebHook;

internal class CreateWebHookCommandHandler : IRequestHandler<CreateWebHookCommand, Result>
{
    private readonly IApplicationsRepository _applicationsRepository;
    private readonly IWebHooksRepository _webHooksRepository;

    public CreateWebHookCommandHandler(IApplicationsRepository applicationsRepository, IWebHooksRepository webHooksRepository)
    {
        _applicationsRepository = applicationsRepository;
        _webHooksRepository = webHooksRepository;
    }

    public async Task<Result> Handle(CreateWebHookCommand request, CancellationToken cancellationToken)
    {
        var sourceApplication = await _applicationsRepository.GetByCodeAsync(request.SourceCode, cancellationToken);
        var clientApplication = await _applicationsRepository.GetByCodeAsync(request.ClientCode, cancellationToken);

        if (sourceApplication.IsFailed || clientApplication.IsFailed)
        {
            //TODO: can add logging in here...
            return Result.Fail(ErrorCodes.DataRetrievalIssues);
        }

        var saved = await _webHooksRepository.SaveAsync(new WebHook
        {
            ClientCode = request.ClientCode,
            SourceCode = request.SourceCode,
            EventCode = request.EventCode,
            TenantCode = request.TenantCode,
            Endpoint = request.Endpoint
        });

        if (saved.IsFailed)
        {
            return Result.Fail(ErrorCodes.GeneralModuleIssues);
        }

        return Result.Ok();
    }
}
