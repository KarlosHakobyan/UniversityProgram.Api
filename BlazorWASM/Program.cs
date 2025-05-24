using BlazorWASM;
using BlazorWASM.Client;
using BlazorWASM.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<CustomAuthMessageHandler>();
builder.Services.AddHttpClient("ServerAPI",
      client => client.BaseAddress = new Uri("http://localhost:5146"))
    .AddHttpMessageHandler<CustomAuthMessageHandler>();

/*builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("ServerAPI"));*/

builder.Services.AddHttpClient<StudentClient>("ServerAPI");

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";

    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]!);

    options.UserOptions.RoleClaim = "https://www.aca.com/roles";
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("namekarlos", policy => policy.RequireClaim("nickname" , "karloshakobyan99"));
    options.AddPolicy("User", policy => policy.RequireClaim("scope", "user"));
});

await builder.Build().RunAsync();