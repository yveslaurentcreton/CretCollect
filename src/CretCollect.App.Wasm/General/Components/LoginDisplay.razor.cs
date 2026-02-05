using System.Security.Claims;
using CretCollect.App.Wasm.General.Models;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace CretCollect.App.Wasm.General.Components;

public partial class LoginDisplay
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IDispatcher Dispatcher { get; set; } = default!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    [Inject] public IOptions<KeycloakOptions> KeycloakOptions { get; set; } = default!;
    
    private bool _open = false;
    private ClaimsPrincipal? _user;
    private string DisplayName => _user?.FindFirst("name")?.Value ?? string.Empty;
    private string Initials = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        await InitializeClaimsPrincipal();
        
        // Dispatcher.Dispatch(new FetchLoggedInWorkerAction());
    }
    
    private async Task InitializeClaimsPrincipal()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        _user = authState.User;
        
        if (_user.Identity?.IsAuthenticated == true)
        {
            var firstname = _user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            var lastname = _user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
            Initials = $"{firstname?[0]}{lastname?[0]}";
        }
    }

    private async Task NavigateToAccount()
    {
        var keycloakOptions = KeycloakOptions.Value;
        var endpoint = keycloakOptions.Endpoint;
        var realm = keycloakOptions.Realm;
        
        var url = $"{endpoint}/realms/{realm}/account";
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    }

    private void LogOut()
    {
        NavigationManager.NavigateTo("authentication/logout", forceLoad: true);
    }
}