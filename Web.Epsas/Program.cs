using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using Web.Epsas.Context.Model;
using Web.Epsas.Context.Services.Implementations;
using Web.Epsas.Context.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ManagerRest>();
builder.Services.AddScoped<LoadingService>();

builder.Services.AddHttpClient("Api", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["EndPoints:WebApi"]);

}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
});
builder.Services.AddLocalization();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ManagerAuthorize>();
builder.Services.AddScoped<AuthenticationStateProvider, ManagerAuthorize>(provider => provider.GetRequiredService<ManagerAuthorize>());
builder.Services.AddScoped<IManagerAuthorize, ManagerAuthorize>(provider => provider.GetRequiredService<ManagerAuthorize>());
builder.Services.AddScoped<IManagerStorage, ManagerStorage>();




builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopEnd;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Text;
});

var app = builder.Build();

app.UseRequestLocalization("es-BO");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();