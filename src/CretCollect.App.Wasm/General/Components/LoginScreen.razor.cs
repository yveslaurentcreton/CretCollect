using Microsoft.AspNetCore.Components;

namespace CretCollect.App.Wasm.General.Components;

public partial class LoginScreen
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    
    private void Login()
    {
        NavigationManager.NavigateTo("authentication/login", forceLoad: true);
    }
}