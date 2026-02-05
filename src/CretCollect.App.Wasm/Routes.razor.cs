using CretNet.Platform.Blazor.State;
using CretNet.Platform.Blazor.State.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace CretCollect.App.Wasm;

public partial class Routes
{
    [Inject] public IDispatcher Dispatcher { get; set; } = default!;
    [Inject] public IState<CnpSiteState> CnpSiteState { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        
        if (CnpSiteState.Value.CurrentCulture is null)
        {
            Dispatcher.Dispatch(new InitializeCultureAction());
        }
    }
}