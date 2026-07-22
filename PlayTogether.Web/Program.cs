using PlayTogether.Web.Components;
using PlayTogether.Web.Services;

var builder = WebApplication.CreateBuilder(args);


// Services HIER hinzufügen
builder.Services.AddSingleton<ClickerService>();
builder.Services.AddSingleton<LobbyService>();
builder.Services.AddHostedService<AutoClickBackgroundService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



var app = builder.Build();


// HTTP Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();