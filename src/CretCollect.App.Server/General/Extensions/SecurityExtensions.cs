using CretCollect.App.Wasm.General.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace CretCollect.App.Server.General.Extensions;

public static class SecurityExtensions
{
    public static void AddCretCollectAppSecurity(this IServiceCollection services, IHostEnvironment environment)
    {
        // Read Keycloak options from configuration
        var serviceProvider = services.BuildServiceProvider();
        var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>();
        
        // Configure OpenID Connect
        var endpoint = keycloakOptions.Value.Endpoint;
        var realm = keycloakOptions.Value.Realm;
        var clientId = keycloakOptions.Value.ClientId;
        var isDevelopment = environment.IsDevelopment();
        
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddOpenIdConnect(options =>
        {
            options.Authority = $"{endpoint}/realms/{realm}";
            options.ClientId = clientId;
            options.ResponseType = OpenIdConnectResponseType.CodeIdTokenToken;
            options.SaveTokens = true;
            options.RequireHttpsMetadata = !isDevelopment;
            options.GetClaimsFromUserInfoEndpoint = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "preferred_username",
                RoleClaimType = "roles",
            };
        });
        
        services.AddAuthorizationBuilder();
        services.AddCascadingAuthenticationState();
    }

    public static void UseCretCollectAppSecurity(this WebApplication app)
    {
        app.MapGet("/authentication/login", () 
            => TypedResults.Challenge(
                new AuthenticationProperties { RedirectUri = "/" }, [OpenIdConnectDefaults.AuthenticationScheme]))
            .AllowAnonymous();
        app.MapGet("/authentication/logout", () 
            => TypedResults.SignOut(
                new AuthenticationProperties { RedirectUri = "/" }, [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme]))
            .AllowAnonymous();
    }
}