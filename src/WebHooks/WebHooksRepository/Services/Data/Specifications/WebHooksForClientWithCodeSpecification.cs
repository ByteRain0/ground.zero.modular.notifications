using Shared.Specifications;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Specifications;

public class WebHooksForClientWithCodeSpecification : Specification<WebHookDataModel>
{
    public WebHooksForClientWithCodeSpecification(string clientCode) :
        base(webHook =>
            string.IsNullOrEmpty(clientCode)
            || webHook.ClientCode.ToLower() == clientCode.ToLower())
    {
    }
}
