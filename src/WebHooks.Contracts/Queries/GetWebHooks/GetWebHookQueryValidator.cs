using FluentValidation;

namespace WebHooks.Contracts.Queries.GetWebHooks;

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
