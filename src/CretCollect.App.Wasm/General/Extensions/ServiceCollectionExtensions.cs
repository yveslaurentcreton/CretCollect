using CretCollect.App.Wasm.General.Services;
using CretNet.Platform.Blazor.Extensions;
using CretNet.Platform.Blazor.Services;
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
        // CretNet Platform
        services.AddCnpBlazor(options =>
        {
            options.UseRouting();
            options.ScanAssemblies(typeof(IAssemblyMarker).Assembly);
            configureFluxor?.Invoke(options);
        });
        
        // Language
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture("en");
            options.AddSupportedCultures("en", "nl");
            options.AddSupportedUICultures("en", "nl");
        });
        
        // Validators
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        
        // Services
        services.AddSingleton<IApplicationService, ApplicationService>();
        services.AddScoped<ITimeService, TimeService>();
        services.AddScoped<IServerTimeProvider, ServerTimeProvider>();
    }
}

internal interface IAssemblyMarker;