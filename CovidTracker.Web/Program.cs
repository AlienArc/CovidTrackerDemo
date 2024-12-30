using CovidTracker.Core.Models;
using CovidTracker.Core.Repositories;
using CovidTracker.Core.Services;
using CovidTracker.Web.Components;
using CovidTracker.Web.ViewModels;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add automapper the container.
builder.Services.AddAutoMapper(typeof(StateDailyTotal));

// Add services to the container.
builder.Services.AddSingleton<ICovidTrackingService, CovidTrackingService>();

// Add repositories to the container.
builder.Services.AddSingleton<ICovidStateDataRepository, CovidStateDataRepository>();

// Add ViewModels to the container.
builder.Services.AddTransient<HomeViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
