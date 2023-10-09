using FluentValidation;

namespace WebHooks.Contracts.Queries.GetWebHook;

public class GetWebHookQueryValidator : AbstractValidator<GetWebHookQuery>
{
    public GetWebHookQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
