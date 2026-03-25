using Estoque.Web.Components;
using Estoque.Web.Services;
using Microsoft.Extensions.Logging.EventLog;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.None);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddHttpClient<IProdutoApiService, ProdutoApiService>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var apiBaseUrl = configuration["Api:BaseUrl"] ?? "https://localhost:7048/";
    client.BaseAddress = new Uri(apiBaseUrl);
});
builder.Services.AddHttpClient<IAuthApiService, AuthApiService>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var apiBaseUrl = configuration["Api:BaseUrl"] ?? "https://localhost:7048/";
    client.BaseAddress = new Uri(apiBaseUrl);
});
builder.Services.AddHttpClient<IMovimentacaoApiService, MovimentacaoApiService>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var apiBaseUrl = configuration["Api:BaseUrl"] ?? "https://localhost:7048/";
    client.BaseAddress = new Uri(apiBaseUrl);
});
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
