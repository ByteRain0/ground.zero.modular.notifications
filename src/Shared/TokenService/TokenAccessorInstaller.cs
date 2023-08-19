using Microsoft.Extensions.DependencyInjection;

namespace Shared.TokenService;

public static class TokenAccessorInstaller
{
    public static IServiceCollection AddTokenAccessor(this IServiceCollection services)
    {
        services.AddSingleton<ITokenService, TokenService>();
        return services;
    }
}
