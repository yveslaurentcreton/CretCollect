using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CretCollect.App.Wasm.Pages;

public partial class Home
{
    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    
    private ClaimsPrincipal? _user;
    private string? _firstname;
    private string? _lastname;
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        await InitializeClaimsPrincipal();
    }
    
    private async Task InitializeClaimsPrincipal()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        _user = authState.User;
        
        if (_user.Identity?.IsAuthenticated == true)
        {
            _firstname = _user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            _lastname = _user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
        }
    }
}