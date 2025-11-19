using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;
using RentalCar.Infrastructure.Persistence.Repositories;
using RentalCar.Infrastructure.Services.Auth;

namespace RentalCar.Infrastructure;

public static class DependencyInjection
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddScoped<ICarAttributeRepository, CarAttributeRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IValueRepository, ValueRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMakeRepository, MakeRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<ICarValueRepository, CarValueRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

    }
}