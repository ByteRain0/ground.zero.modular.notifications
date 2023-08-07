using FluentValidation;

namespace WebHooks.Manager.Contracts.Queries;

internal class GetWebHookQueryValidator : AbstractValidator<GetWebHooksQuery>
{
    public GetWebHookQueryValidator()
    {
        RuleFor(x => x.SourceCode)
            .NotEmpty();

        RuleFor(x => x.EventCode)
            .NotEmpty();

        RuleFor(x => x.TennantCode)
            .NotEmpty();
    }
}
