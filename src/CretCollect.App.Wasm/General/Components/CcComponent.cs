using CretNet.Platform.Blazor.Components;
using CretNet.Platform.Blazor.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace CretCollect.App.Wasm.General.Components;

public class CcComponent : CnpComponent
{
    [Inject] public IStringLocalizer<CnpLabels> CnpLocalizer { get; set; } = default!;
    [Inject] public IStringLocalizer<Labels> Localizer { get; set; } = default!;
}