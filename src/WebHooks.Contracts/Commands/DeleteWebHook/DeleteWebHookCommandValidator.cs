using FluentValidation;

namespace WebHooks.Contracts.Commands.DeleteWebHook;

internal class DeleteWebHookCommandValidator : AbstractValidator<DeleteWebHookCommand>
{
    public DeleteWebHookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
