using Blazored.LocalStorage;
using CretCollect.App.Server.Components;
using CretCollect.App.Server.General.Extensions;
using CretCollect.App.Wasm.General.Extensions;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.AddCretCollectConfiguration(builder.Configuration);

// Aspire
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Security
builder.Services.AddCretCollectAppSecurity(builder.Environment);

// FluentUI
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();

// App
builder.Services.AddAppServices(
    options => options.ScanAssemblies(typeof(Program).Assembly, typeof(CretNet.Platform.Blazor.Server._Imports).Assembly));
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCretCollectAppSecurity();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CretCollect.App.Wasm._Imports).Assembly);

app.Run();
