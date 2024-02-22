using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Persistence.DependencyInjection;
using Persistence.Contexts;
using Application.Services.Repositories;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("BaseDb"));

        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RentACarInventoryDb")));

        services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());


        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IFuelRepository, FuelRepository>();
        services.AddScoped<ITransmissionRepository, TransmissionRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        return services;
    }
}
