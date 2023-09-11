using Shared.Specifications;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Specifications;

public class WebHooksForApplicationWithCodeSpecification : Specification<WebHookDataModel>
{
    public WebHooksForApplicationWithCodeSpecification(string applicationCode)
        : base(webHook => webHook.SourceCode.ToLower() == applicationCode.ToLower())
    {
    }
}
