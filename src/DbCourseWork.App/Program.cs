using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Data;
using Services;
using WebUI;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddRepositories();
builder.Services.AddServices();

AddBlazorise(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
// app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
return;


void AddBlazorise(IServiceCollection services)
{
    services.AddBlazorise();
    services.AddBootstrapProviders().AddFontAwesomeIcons().AddBootstrapComponents();
}