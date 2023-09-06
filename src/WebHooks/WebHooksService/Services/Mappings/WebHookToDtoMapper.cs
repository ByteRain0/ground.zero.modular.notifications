using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Contracts.Models;

namespace WebHooks.WebHooksService.Services.Mappings;

public static class WebHookToDtoMapper
{
    public static WebHookDto ToDto(this WebHook webHook)
    {
        return new WebHookDto
        {
            Id = webHook.Id,
            EventCode = webHook.EventCode,
            SourceCode = webHook.SourceCode,
            Endpoint = webHook.Endpoint,
            TenantCode = webHook.TenantCode,
            ClientCode = webHook.ClientCode
        };
    }
}
