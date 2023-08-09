using ApplicationRegistry.Contracts;
using MediatR;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Contracts.Commands.CreateWebHook;

namespace WebHooks.WebHooksService.Services.CommandHandlers.CreateWebHook;

internal class CreateWebHookCommandHandler : IRequestHandler<CreateWebHookCommand>
{
    private readonly IApplicationsRepository _applicationsRepository;
    private readonly IWebHooksAccessor _webHooksAccessor;

    public CreateWebHookCommandHandler(IApplicationsRepository applicationsRepository, IWebHooksAccessor webHooksAccessor)
    {
        _applicationsRepository = applicationsRepository;
        _webHooksAccessor = webHooksAccessor;
    }

    public async Task Handle(CreateWebHookCommand request, CancellationToken cancellationToken)
    {
        var sourceApplication = await _applicationsRepository.GetByCodeAsync(request.SourceCode, cancellationToken);
        var clientApplication = await _applicationsRepository.GetByCodeAsync(request.ClientCode, cancellationToken);

        // For now check both at the same time.
        if (sourceApplication is null || clientApplication is null)
        {
            //TODO: later refactor this to a Result/ErrorOr pattern or at least custom exception
            throw new InvalidOperationException("Application not found");
        }

        var saved = await _webHooksAccessor.SaveAsync(new WebHook
        {
            ClientCode = request.ClientCode,
            SourceCode = request.SourceCode,
            EventCode = request.EventCode,
            TenantCode = request.TenantCode,
            Endpoint = request.Endpoint
        });

        if (!saved)
        {
            //TODO: later refactor this to a Result/ErrorOr pattern or at least custom exception
            throw new InvalidOperationException("Failure during webhooks save");
        }
    }
}
