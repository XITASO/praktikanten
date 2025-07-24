using PlayTogether.Web.Components;
using PlayTogether.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the application.
// Services can be injected into constructors of other services or be injected into pages.
// Available service types are:
//    * AddSingleton: Is only created once for the whole application
//    * AddTransient: Is always created from scratch when injected in constructors or pages
//    * AddScoped: Is created once for each tab (or browser). Each new tab (or browser) gets a separate instance.

builder.Services.AddSingleton<CounterService>();

// ? ADDED: Register GameService as a singleton so all users share the same game state
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<GameService2>();
builder.Services.AddSingleton<GameService3>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();