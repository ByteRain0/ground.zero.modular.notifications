using FluentResults;
using MediatR;
using WebHooks.Contracts.Commands.DeleteWebHook;
using WebHooks.WebHooksRepository.Contracts;
using ErrorCodes = WebHooks.Contracts.ErrorCodes;

namespace WebHooks.WebHooksService.Services.Handlers.CommandHandlers.DeleteWebHook;

public class DeleteWebHookCommandHandler : IRequestHandler<DeleteWebHookCommand, Result>
{
    private readonly IWebHooksRepository _webHooksRepository;

    public DeleteWebHookCommandHandler(IWebHooksRepository webHooksRepository)
    {
        _webHooksRepository = webHooksRepository;
    }

    public async Task<Result> Handle(DeleteWebHookCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _webHooksRepository.DeleteAsync(request.Id.ToString());

        if (deleted.IsFailed)
        {
            //Usually you would not expose the inner error codes to the API consumers so you would remap it to some error code.
            //And log all the data you will need for debug.
            return Result.Fail(ErrorCodes.GeneralModuleIssues);
        }

        return Result.Ok();
    }
}
