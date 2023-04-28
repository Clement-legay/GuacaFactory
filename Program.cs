using Blazored.SessionStorage;
using GuacaFactory;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient(
        new HttpClientHandler
        {
            MaxRequestContentBufferSize = 1024 * 1024 * 10 // 10 MB
        })
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddInjections();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();