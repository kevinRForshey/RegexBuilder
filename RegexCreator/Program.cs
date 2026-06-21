using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Regex.Core;
using RegexCreator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IRegexPresetProvider, RegexPresetProvider>();
builder.Services.AddSingleton<IRuleFactory, RuleFactory>();
builder.Services.AddSingleton<IRegexComposer, RegexComposer>();
builder.Services.AddSingleton<IRegexTester, RegexTester>();
await builder.Build().RunAsync();
