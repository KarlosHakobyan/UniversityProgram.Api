using BlazorWASM;
using BlazorWASM.Apis;
using BlazorWASM.Client;
using BlazorWASM.Constants;
using BlazorWASM.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using Polly;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<CustomAuthMessageHandler>();

builder.Services.AddRefitClient<IStudentApi>()
.ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("http://localhost:5146");
})
.AddHttpMessageHandler<CustomAuthMessageHandler>()
.AddTransientHttpErrorPolicy(p =>
        p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(Math.Pow(2, attempt))));

/*builder.Services.AddHttpClient("ServerAPI",
      client => client.BaseAddress = new Uri("http://localhost:5146"))
    .AddHttpMessageHandler<CustomAuthMessageHandler>().AddTransientHttpErrorPolicy(p =>
     p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(Math.Pow(2, attempt))));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("ServerAPI"));

builder.Services.AddHttpClient<StudentClient>("ServerAPI")
.AddTransientHttpErrorPolicy(p =>
 p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(Math.Pow(2, attempt))));
*/


builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";

    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]!);

    options.UserOptions.RoleClaim = AuthConstants.RoleType ;
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy(AuthConstants.NamePolicy, policy => policy.RequireClaim("nickname", "karloshakobyan99"));
 
});

await builder.Build().RunAsync();