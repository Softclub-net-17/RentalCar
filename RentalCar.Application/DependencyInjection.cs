using Microsoft.Extensions.DependencyInjection;
using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.DTOS;
using RentalCar.Application.CarAttributes.Handlers;
using RentalCar.Application.CarAttributes.Queries;
using RentalCar.Application.CarAttributes.Validators;
using RentalCar.Application.Categories.Commands;
using RentalCar.Application.Categories.DTOs;
using RentalCar.Application.Categories.Handlers;
using RentalCar.Application.Categories.Queries;
using RentalCar.Application.Categories.Validators;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
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

        //validators
        services.AddScoped<IValidator<CarAttributeCreateCommand>, CarAttributeCreateValidator>();
        services.AddScoped<IValidator<CarAttributeUpdateCommand>, CarAttributeUpdateValidator>();
        services.AddScoped<IValidator<ValueCreateCommand>, ValueCreateValidator>();
        services.AddScoped<IValidator<ValueUpdateCommand>, ValueUpdateValidator>();
        services.AddScoped<IValidator<CategoryCreateCommand>, CategoryCreateValidator>();
        services.AddScoped<IValidator<CategoryUpdateCommand>, CategoryUpdateValidator>();
        services.AddScoped<IValidator<MakeCreateCommand>, MakeCreateValidator>();
        services.AddScoped<IValidator<MakeUpdateCommand>, MakeUpdateValidator>();
        services.AddScoped<IValidator<ModelCreateCommand>, ModelCreateValidator>();
        services.AddScoped<IValidator<ModelUpdateCommand>, ModelUpdateValidator>();

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
    }
}