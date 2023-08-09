using FluentValidation;

namespace WebHooks.WebHooksService.Contracts.Commands.DeleteWebHook;

internal class DeleteWebHookCommandValidator : AbstractValidator<DeleteWebHookCommand>
{
    public DeleteWebHookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
