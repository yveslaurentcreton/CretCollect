using CretCollect.App.Wasm.General.Components;
using CretCollect.App.Wasm.General.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using GeneralLabels = CretCollect.App.Wasm.Resources.General.Labels;

namespace CretCollect.App.Wasm.General.Layouts;

public partial class MainLayout
{
    [Inject] public IApplicationService ApplicationService { get; set; } = default!;
    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = default!;
    
    public bool? IsConnectedWithApi = true;
    
    private async Task OpenInfo()
    {
        var url = "/docs";
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    }
    
    private async Task OpenSiteSettings()
    {
        var dialog = await DialogService.ShowPanelAsync<SiteSettingsPanel>(new DialogParameters()
        {
            Alignment = HorizontalAlignment.Right,
            Title = GeneralLabels.SiteSettings,
            PrimaryAction = "OK",
            SecondaryAction = null,
        });
        var result = await dialog.Result;
    }
}