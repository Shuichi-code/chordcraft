using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ChordCraft.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<ChordCraft.Client.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AudioService>();
builder.Services.AddScoped<LessonDataService>();
builder.Services.AddScoped<LocalProgressService>();
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<ApiProgressService>();
builder.Services.AddScoped<IClientProgressService, ProgressRouterService>();

await builder.Build().RunAsync();
