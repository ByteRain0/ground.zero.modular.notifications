using FluentValidation;

namespace WebHooks.WebHooksService.Contracts.Queries.GetWebHooks;

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
