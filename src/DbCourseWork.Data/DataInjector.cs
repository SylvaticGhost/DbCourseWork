using Dapper;
using Data.Context;
using Data.Repositories;
using Data.UnitOfWork;
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
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IRouteReportRepository, RouteReportRepository>();
    }
}