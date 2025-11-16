using Microsoft.Extensions.DependencyInjection;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.Handlers;
using RentalCar.Application.Auth.Validators;
using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Handlers;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.CarAttributes.Validators;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.Commands;
using RentalCar.Application.Users.DTOS;
using RentalCar.Application.Users.Handlers;
using RentalCar.Application.Users.Queries;
using RentalCar.Application.Users.Validators;
using RentalCar.Application.Values.Commands;
using RentalCar.Application.Values.DTOS;
using RentalCar.Application.Values.Handlers;
using RentalCar.Application.Values.Queries;
using RentalCar.Application.Values.Validators;

namespace RentalCar.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<CarAttributeGetQuery, Result<List<CarAttributeGetDto>>>, CarAttributeGetQueryHandler>();
        services.AddScoped<IQueryHandler<CarAttributeGetByIdQuery, Result<CarAttributeGetDto>>, CarAttributeGetByIdQueryHandler>();
        services.AddScoped<ICommandHandler<CarAttributeCreateCommand, Result<string>>, CarAttributeCreateCommandHandler>();
        services.AddScoped<ICommandHandler<CarAttributeUpdateCommand, Result<string>>, CarAttributeUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<CarAttributeDeleteCommand, Result<string>>, CarAttributeDeleteCommandHandler>();

        services.AddScoped<IQueryHandler<ValueGetQuery, Result<List<ValueGetDto>>>, ValueGetQueryHandler>();
        services.AddScoped<IQueryHandler<ValueGetByIdQuery, Result<ValueGetDto>>, ValueGetByIdQueryHandler>();
        services.AddScoped<ICommandHandler<ValueCreateCommand, Result<string>>, ValueCreateCommandHandler>();
        services.AddScoped<ICommandHandler<ValueUpdateCommand, Result<string>>, ValueUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<ValueDeleteCommand, Result<string>>, ValueDeleteCommandHandler>();
        
        services.AddScoped<IQueryHandler<UserGetQuery, Result<List<UserGetDto>>>, UserGetQueryHandler>();
        services.AddScoped<IQueryHandler<UserGetByIdQuery, Result<UserGetDto>>, UserGetByIdQueryHandler>();
        services.AddScoped<ICommandHandler<UserUpdateCommand, Result<string>>, UserUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<UserDeleteCommand, Result<string>>, UserDeleteCommandHandler>();

        services.AddScoped<ICommandHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();
        services.AddScoped<ICommandHandler<LoginCommand, Result<string>>, LoginCommandHandler>();

        services.AddScoped<IValidator<CarAttributeCreateCommand>, CarAttributeCreateValidator>();
        services.AddScoped<IValidator<CarAttributeUpdateCommand>, CarAttributeUpdateValidator>();
        services.AddScoped<IValidator<ValueCreateCommand>, ValueCreateValidator>();
        services.AddScoped<IValidator<ValueUpdateCommand>, ValueUpdateValidator>();
        services.AddScoped<IValidator<UserUpdateCommand>, UserUpdateValidator>();
        services.AddScoped<IValidator<RegisterCommand>, RegisterValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
    }
}