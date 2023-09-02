using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Mappings;

public static class WebHookContractToDataModelMapper
{
    public static WebHookDataModel ToDataModel(this WebHook model)
    {
        return new WebHookDataModel
        {
            Id = model.Id,
            Endpoint = model.Endpoint.ToString(),
            ClientCode = model.ClientCode,
            EventCode = model.EventCode,
            SourceCode = model.SourceCode,
            TenantCode = model.TenantCode
        };
    }

    public static WebHook ToContract(this WebHookDataModel model)
    {
        return new WebHook
        {
            Id = model.Id,
            Endpoint = new Uri(model.Endpoint),
            ClientCode = model.ClientCode,
            EventCode = model.EventCode,
            SourceCode = model.SourceCode,
            TenantCode = model.TenantCode
        };
    }
}
