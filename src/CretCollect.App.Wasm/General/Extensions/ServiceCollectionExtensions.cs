using CretCollect.App.Wasm.General.Services;
using CretNet.Platform.Blazor.Services;
using CretNet.Platform.Blazor.Services.Countries;
using FluentValidation;
using Fluxor;
using Fluxor.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace CretCollect.App.Wasm.General.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAppServices(
        this IServiceCollection services,
        Action<FluxorOptions>? configureFluxor = null)
    {
        // Language
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture("en");
            options.AddSupportedCultures("en", "nl");
            options.AddSupportedUICultures("en", "nl");
        });
        
        // Fluxor
        services.AddFluxor(options =>
        {
            options.UseRouting();
            options.ScanAssemblies(typeof(Program).Assembly);
            configureFluxor?.Invoke(options);
        });
        
        // Validators
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        
        // Services
        services.AddSingleton<IApplicationService, ApplicationService>();
        services.AddScoped<IBreadcrumbService, BreadcrumbService>();
        services.AddScoped<ICnpSectionService, CnpSectionService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICnpToastService, CnpToastService>();
    }
}