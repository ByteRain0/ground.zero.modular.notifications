using Shared.Specifications;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Specifications;

public class WebHooksForEventWithCodeSpecification : Specification<WebHookDataModel>
{
    public WebHooksForEventWithCodeSpecification(string eventCode)
        : base(webHook =>
            string.IsNullOrEmpty(eventCode)
            || webHook.EventCode.ToLower() == eventCode.ToLower())
    {
    }
}
