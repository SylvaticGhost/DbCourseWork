using Dapper;
using DbCourseWork.Components;
using DbCourseWork.Data;
using DbCourseWork.Repositories;
using DbCourseWork.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServerSideBlazor();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped<DataContext>();
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<ICardOwnerRepository, CardOwnerRepository>();
builder.Services.AddScoped<ICardOwnerService, CardOwnerService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();