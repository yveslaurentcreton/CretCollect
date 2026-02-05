using CretCollect.App.Wasm.General.Models;

namespace CretCollect.App.Server.General.Extensions;

public static class ConfigurationExtensions
{
    public static void AddCretCollectConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeycloakOptions>(configuration.GetSection("CretCollect:Security:Keycloak"));
    }
}