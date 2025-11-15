using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarAttributes.Handlers;

public class CarAttributeCreateCommandHandler(ICarAttributeRepository carAttributeRepository, 
    IUnitOfWork unitOfWork,
    IValidator<CarAttributeCreateCommand> validator) : ICommandHandler<CarAttributeCreateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CarAttributeCreateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }

        var carAttribute = command.ToEntity();

        await carAttributeRepository.CreateAsync(carAttribute);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "CarAttribute created successfully.");
    }
}