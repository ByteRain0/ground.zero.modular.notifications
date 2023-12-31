using Shared.Specifications;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Specifications;

public class WebHooksForApplicationWithCodeSpecification : Specification<WebHookDataModel>
{
    public WebHooksForApplicationWithCodeSpecification(string applicationCode)
        : base(webHook =>
            string.IsNullOrEmpty(applicationCode)
            || webHook.SourceCode.ToLower() == applicationCode.ToLower())
    {
    }
}
