using System.Globalization;
using CretNet.Platform.Blazor.State;
using CretNet.Platform.Blazor.State.Actions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;

namespace CretCollect.App.Wasm.General.Components;

public partial class SiteSettingsPanel : IDialogContentComponent
{
    private string _languageControlKey = Guid.NewGuid().ToString();
    
    [Inject] private IDispatcher Dispatcher { get; set; } = default!;
    [Inject] private IState<CnpSiteState> CnpSiteState { get; set; } = default!;
    
    public IEnumerable<DesignThemeModes> Themes => Enum.GetValues<DesignThemeModes>();
    public DesignThemeModes Theme { get; set; }
    public OfficeColor? OfficeColor { get; set; }
    public string? CustomColor { get; set; }
    public string RectangleColor => (string.IsNullOrEmpty(CustomColor) ? GetCustomColor(OfficeColor) : CustomColor) ?? string.Empty;

    public List<CultureInfo> Locales { get; set; }
    public CultureInfo? Locale { get; set; }

    public SiteSettingsPanel()
    {
        Locales = GetSupportedLanguages();
    }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        CnpSiteState.StateChanged += (_, _) => _languageControlKey = Guid.NewGuid().ToString(); // This does a force update (statehaschanged not working)
        Locale = Locales.FirstOrDefault(x => x.Name == CnpSiteState.Value.CurrentCulture?.Name);
    }

    private static string? GetCustomColor(OfficeColor? color)
    {
        return color switch
        {
            null => OfficeColorUtilities.GetRandom(true).ToAttributeValue(),
            Microsoft.FluentUI.AspNetCore.Components.OfficeColor.Default => "#036ac4",
            _ => color.ToAttributeValue(),
        };
    }
    
    private void SetLanguage()
    {
        if (Locale is null)
            return;
        
        Dispatcher.Dispatch(new ChangeCultureAction(Locale));
    }
    
    public List<CultureInfo> GetSupportedLanguages()
    {
        var supportedLanguages = new[] { "en", "nl" };
        var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
        var supportedCultures = cultures
            .Where(c => supportedLanguages.Contains(c.TwoLetterISOLanguageName))
            .ToList();

        return supportedCultures;
    }
}