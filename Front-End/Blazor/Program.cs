using Blazor;
using Blazor.Application.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Cliente padrão (Sem Token) - Usado no AuthService
builder.Services.AddHttpClient("Publico", client => {
    client.BaseAddress = new Uri("http://localhost:5248/");
});

// Cliente com Token - Usado no ProdutoService
builder.Services.AddHttpClient("Privado", client => {
    client.BaseAddress = new Uri("http://localhost:5248/");
}).AddHttpMessageHandler<JwtHandler>();

// Isso define que, por padrão, se alguém pedir um HttpClient, 
// o Blazor entregará o "Privado" (com token).
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Privado"));

// Registra o JwtHandler como Scoped
builder.Services.AddScoped<JwtHandler>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CarrinhoService>();

await builder.Build().RunAsync();
