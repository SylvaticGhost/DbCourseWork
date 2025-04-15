using Dapper;
using DbCourseWork.Data;
using DbCourseWork.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DataInjector
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<DataContext>();
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<ICardOwnerRepository, CardOwnerRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
        services.AddScoped<ICardOperationRepository, CardOperationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRouteReportRepository, RouteReportRepository>();
    }
}