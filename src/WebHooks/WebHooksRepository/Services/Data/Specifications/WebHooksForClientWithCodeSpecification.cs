using Shared.Specifications;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Specifications;

public class WebHooksForClientWithCodeSpecification : Specification<WebHookDataModel>
{
    public WebHooksForClientWithCodeSpecification(string clientCode) :
        base(webhook => webhook.ClientCode.ToLower() == clientCode.ToLower())
    {
    }
}
