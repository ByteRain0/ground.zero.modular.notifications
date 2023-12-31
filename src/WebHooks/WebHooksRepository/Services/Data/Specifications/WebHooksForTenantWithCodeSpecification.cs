using Shared.Specifications;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Specifications;

public class WebHooksForTenantSpecification : Specification<WebHookDataModel>
{
    public WebHooksForTenantSpecification(string tenantCode)
        : base(webHook =>
            string.IsNullOrEmpty(tenantCode)
            || webHook.TenantCode.ToLower() == tenantCode.ToLower())
    {
    }
}
