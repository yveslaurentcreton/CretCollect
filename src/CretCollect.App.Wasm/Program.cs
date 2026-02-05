using CretCollect.App.Wasm.General.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAppServices();

await builder.Build().RunAsync();
