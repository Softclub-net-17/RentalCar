using Microsoft.Extensions.DependencyInjection;
using RentalCar.Domain.Interfaces;
using RentalCar.Infrastructure.Persistence.Repositories;

namespace RentalCar.Infrastructure;

public static class DependencyInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICarAttributeRepository, CarAttributeRepository>();
        services.AddScoped<IValueRepository, ValueRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMakeRepository, MakeRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();

    }
}