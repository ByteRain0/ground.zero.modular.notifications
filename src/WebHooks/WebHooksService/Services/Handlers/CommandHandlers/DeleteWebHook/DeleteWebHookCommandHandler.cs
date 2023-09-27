using MediatR;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Contracts.Commands.DeleteWebHook;

namespace WebHooks.WebHooksService.Services.Handlers.CommandHandlers.DeleteWebHook;

public class DeleteWebHookCommandHandler : IRequestHandler<DeleteWebHookCommand>
{
    private readonly IWebHooksRepository _webHooksRepository;

    public DeleteWebHookCommandHandler(IWebHooksRepository webHooksRepository)
    {
        _webHooksRepository = webHooksRepository;
    }

    public async Task Handle(DeleteWebHookCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _webHooksRepository.DeleteAsync(request.Id.ToString());

        if (!deleted)
        {
            throw new InvalidOperationException("Error encountered during deletion of WebHook, try again later.");
        }
    }
}
