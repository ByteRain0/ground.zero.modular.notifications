using FluentValidation;

namespace WebHooks.Manager.Contracts.Commands.CreateWebHook;

internal class CreateWebHookCommandValidator : AbstractValidator<CreateWebHookCommand>
{
    public CreateWebHookCommandValidator()
    {
        RuleFor(x => x.Endpoint)
            .NotEmpty();

        RuleFor(x => x.ClientCode)
            .NotEmpty();

        RuleFor(x => x.SourceCode)
            .NotEmpty();

        RuleFor(x => x.EventCode)
            .NotEmpty();

        RuleFor(x => x.TenantCode)
            .NotEmpty();
    }
}
