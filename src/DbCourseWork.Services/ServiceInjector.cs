using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class ServiceInjector
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<ICardOwnerService, CardOwnerService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IDataImportService, DataImportService>();
        services.AddScoped<IRouteReportService, RouteReportService>();
    }
    
}