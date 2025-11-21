using Microsoft.Extensions.DependencyInjection;
using RentalCar.Application.Auth.Commands;
using RentalCar.Application.Auth.Handlers;
using RentalCar.Application.Auth.Validators;
using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Handlers;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.CarAttributes.Validators;
using RentalCar.Application.CarValues.Commands;
using RentalCar.Application.CarValues.Handlers;
using RentalCar.Application.CarValues.Validator;
using RentalCar.Application.Categories.Commands;
using RentalCar.Application.Categories.DTOs;
using RentalCar.Application.Categories.Handlers;
using RentalCar.Application.Categories.Queries;
using RentalCar.Application.Categories.Validators;
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
using RentalCar.Application.Reservations.Commands;
using RentalCar.Application.Reservations.DTOS;
using RentalCar.Application.Reservations.Handlers;
using RentalCar.Application.Reservations.Queries;
using RentalCar.Application.Reservations.Validator;
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
        // car attributes
        services.AddScoped<IQueryHandler<CarAttributeGetQuery, Result<List<CarAttributeGetDto>>>, CarAttributeGetQueryHandler>();
        services.AddScoped<IQueryHandler<CarAttributeGetByIdQuery, Result<CarAttributeGetDto>>, CarAttributeGetByIdQueryHandler>();
        services.AddScoped<ICommandHandler<CarAttributeCreateCommand, Result<string>>, CarAttributeCreateCommandHandler>();
        services.AddScoped<ICommandHandler<CarAttributeUpdateCommand, Result<string>>, CarAttributeUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<CarAttributeDeleteCommand, Result<string>>, CarAttributeDeleteCommandHandler>();
        
        // reservation
        services.AddScoped<IQueryHandler<ReservationGetQuery, Result<List<ReservationGetDto>>>, ReservationGetQueryHandler>();
        services.AddScoped<IQueryHandler<ReservationGetByIdQuery, Result<ReservationGetDto>>, ReservationGetByIdQueryHandler>();
        services.AddScoped<ICommandHandler<ReservationCreateCommand, Result<string>>, ReservationCreateCommandHandler>();
        services.AddScoped<ICommandHandler<ReservationUpdateCommand, Result<string>>, ReservationUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<ReservationDeleteCommand, Result<string>>, ReservationDeleteCommandHandler>();
        
        // car values
        services.AddScoped<ICommandHandler<CarValueCreateCommand, Result<string>>, CarValueCreateCommandHandler>();
        services.AddScoped<ICommandHandler<CarValueUpdateCommand, Result<string>>, CarValueUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<CarValueDeleteCommand, Result<string>>, CarValueDeleteCommandHandler>();
        
        // values
        services.AddScoped<IQueryHandler<ValueGetQuery, Result<List<ValueGetDto>>>, ValueGetQueryHandler>();
        services.AddScoped<IQueryHandler<ValueGetByIdQuery, Result<ValueGetDto>>, ValueGetByIdQueryHandler>();
        services.AddScoped<ICommandHandler<ValueCreateCommand, Result<string>>, ValueCreateCommandHandler>();
        services.AddScoped<ICommandHandler<ValueUpdateCommand, Result<string>>, ValueUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<ValueDeleteCommand, Result<string>>, ValueDeleteCommandHandler>();
        
        // users
        services.AddScoped<IQueryHandler<UserGetQuery, Result<List<UserGetDto>>>, UserGetQueryHandler>();
        services.AddScoped<IQueryHandler<UserGetByIdQuery, Result<UserGetDto>>, UserGetByIdQueryHandler>();
        services.AddScoped<ICommandHandler<UserUpdateCommand, Result<string>>, UserUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<UserDeleteCommand, Result<string>>, UserDeleteCommandHandler>();

        // register
        services.AddScoped<ICommandHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();
        services.AddScoped<ICommandHandler<LoginCommand, Result<string>>, LoginCommandHandler>();
        
        //validators
        services.AddScoped<IValidator<CarAttributeCreateCommand>, CarAttributeCreateValidator>();
        services.AddScoped<IValidator<CarAttributeUpdateCommand>, CarAttributeUpdateValidator>();
        services.AddScoped<IValidator<ReservationCreateCommand>, ReservationCreateValidator>();
        services.AddScoped<IValidator<ReservationUpdateCommand>, ReservationUpdateValidator>();
        services.AddScoped<IValidator<CarValueCreateCommand>, CarValueCreateValidator>();
        services.AddScoped<IValidator<CarValueUpdateCommand>, CarValueUpdateValidator>();
        services.AddScoped<IValidator<ValueCreateCommand>, ValueCreateValidator>();
        services.AddScoped<IValidator<ValueUpdateCommand>, ValueUpdateValidator>();
        services.AddScoped<IValidator<UserUpdateCommand>, UserUpdateValidator>();
        services.AddScoped<IValidator<RegisterCommand>, RegisterValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
        services.AddScoped<IValidator<CategoryCreateCommand>, CategoryCreateValidator>();
        services.AddScoped<IValidator<CategoryUpdateCommand>, CategoryUpdateValidator>();
        services.AddScoped<IValidator<MakeCreateCommand>, MakeCreateValidator>();
        services.AddScoped<IValidator<MakeUpdateCommand>, MakeUpdateValidator>();
        services.AddScoped<IValidator<ModelCreateCommand>, ModelCreateValidator>();
        services.AddScoped<IValidator<ModelUpdateCommand>, ModelUpdateValidator>();
        services.AddScoped<IValidator<CarCreateCommand>, CarCreateValidator>();
        services.AddScoped<IValidator<CarUpdateCommand>, CarUpdateValidator>();

        //categories
        services.AddScoped<ICommandHandler<CategoryCreateCommand, Result<string>>, CategoryCreateCommandHandler>();
        services.AddScoped<ICommandHandler<CategoryUpdateCommand, Result<string>>, CategoryUpdateCommandHandler>();
        services.AddScoped<ICommandHandler<CategoryActivateCommand, Result<string>>, CategoryActivateCommandHandler>();
        services.AddScoped<ICommandHandler<CategoryDeactivateCommand, Result<string>>, CategoryDeactivateCommandHandler>();
        services.AddScoped<IQueryHandler<CategoriesGetQuery, Result<List<CategoryGetDto>>>, CategoryGetQueryHandler>();

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
        services.AddScoped<IQueryHandler<CarsGetByIdQuery, Result<CarsGetDto>>, CarsGetByIdQueryHandler>();


        //images
        services.AddScoped<ICommandHandler<ImageCreateCommand, Result<string>>, ImageCreateCommandHandler>();
        services.AddScoped<ICommandHandler<ImageDeleteCommand, Result<string>>, ImageDeleteCommandHandler>();
        
    }
}
