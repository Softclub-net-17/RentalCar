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
using RentalCar.Application.Makes.Commands;
using RentalCar.Application.Makes.DTOs;
using RentalCar.Application.Makes.Handlers;
using RentalCar.Application.Makes.Queries;
using RentalCar.Application.Makes.Validators;
using RentalCar.Application.Models.Commands;
using RentalCar.Application.Models.DTOs;
using RentalCar.Application.Models.Handlers;
using RentalCar.Application.Models.Queries;
using RentalCar.Application.Models.Validators;
using RentalCar.Application.Values.Commands;
using RentalCar.Application.Values.DTOS;
using RentalCar.Application.Values.Handlers;
using RentalCar.Application.Values.Queries;
using RentalCar.Application.Values.Validators;
using RentalCar.Application.Cars.Commands;
using RentalCar.Application.Cars.Handlers;
using RentalCar.Application.Cars.Queries;
using RentalCar.Application.Cars.DTOs;
using RentalCar.Application.Cars.Validators;
using RentalCar.Application.Images.Commands;
using RentalCar.Application.Images.Handlers;

namespace RentalCar.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<CarAttributeGetQuery, Result<List<CarAttributeGetDto>>>, CarAttributeGetQueryHandler>();
        services.AddScoped<IQueryHandler<CarGetAttributesWithValuesQuery, Result<List<GetCarAttributesWithValuesDto>>>, GetCarAttributesWithValuesHandler>();
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
        services.AddScoped<IQueryHandler<UserGetMeQuery, Result<UserGetDto>>, UserGetMeQueryHandler>();

        services.AddScoped<ICommandHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();
        services.AddScoped<ICommandHandler<LoginCommand, Result<string>>, LoginCommandHandler>();

        //validators
        services.AddScoped<IValidator<CarAttributeCreateCommand>, CarAttributeCreateValidator>();
        services.AddScoped<IValidator<CarAttributeUpdateCommand>, CarAttributeUpdateValidator>();
        services.AddScoped<IValidator<ValueCreateCommand>, ValueCreateValidator>();
        services.AddScoped<IValidator<ValueUpdateCommand>, ValueUpdateValidator>();
        services.AddScoped<IValidator<UserUpdateCommand>, UserUpdateValidator>();
        services.AddScoped<IValidator<RegisterCommand>, RegisterValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
        services.AddScoped<IValidator<MakeCreateCommand>, MakeCreateValidator>();
        services.AddScoped<IValidator<MakeUpdateCommand>, MakeUpdateValidator>();
        services.AddScoped<IValidator<ModelCreateCommand>, ModelCreateValidator>();
        services.AddScoped<IValidator<ModelUpdateCommand>, ModelUpdateValidator>();
        services.AddScoped<IValidator<CarCreateCommand>, CarCreateValidator>();
        services.AddScoped<IValidator<CarUpdateCommand>, CarUpdateValidator>();

        //makes
        services.AddScoped<ICommandHandler<MakeCreateCommand, Result<string>>, MakeCreateCommandHandler>();
        services.AddScoped<ICommandHandler<MakeUpdateCommand, Result<string>>, MakeUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<MakeActivateCommand, Result<string>>, MakeActivateCommandHandler>();
        services.AddScoped<ICommandHandler<MakeDeactivateCommand, Result<string>>, MakeDeactivateCommandHandler>();
        services.AddScoped<IQueryHandler<MakeGetQuery, Result<List<MakeGetDto>>>, MakeGetQueryHandler>();

        //models
        services.AddScoped<ICommandHandler<ModelCreateCommand, Result<string>>, ModelCreateCommandHandler>();
        services.AddScoped<ICommandHandler<ModelUpdateCommand, Result<string>>, ModelUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<ModelDeleteCommand, Result<string>>, ModelDeleteCommandHandler>();
        services.AddScoped<IQueryHandler<ModelsGetQuery, Result<List<ModelGetDto>>>, ModelGetQueryHandler>();

        //cars
        services.AddScoped<ICommandHandler<CarCreateCommand, Result<string>>, CarCreateCommandHandler>();
        services.AddScoped<ICommandHandler<CarUpdateCommand, Result<string>>, CarUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<CarDeleteCommand, Result<string>>, CarDeleteCommandHandler>();
        services.AddScoped<IQueryHandler<CarsGetQuery, Result<List<CarsGetDto>>>, CarGetQueryHandler>();

        //images
        services.AddScoped<ICommandHandler<ImageCreateCommand, Result<string>>, ImageCreateCommandHandler>();
        services.AddScoped<ICommandHandler<ImageDeleteCommand, Result<string>>, ImageDeleteCommandHandler>();

    }
}