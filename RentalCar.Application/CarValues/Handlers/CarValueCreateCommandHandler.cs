using RentalCar.Application.CarValues.Commands;
using RentalCar.Application.CarValues.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarValues.Handlers;

public class CarValueCreateCommandHandler(
    ICarValueRepository carValueRepository,
    ICarRepository carRepository,
    IValueRepository valueRepository,
    IUnitOfWork unitOfWork,
    IValidator<CarValueCreateCommand> validator) : ICommandHandler<CarValueCreateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CarValueCreateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var carValueExists = await carValueRepository.AnyAsync(command.CarId, command.ValueId);

        if (carValueExists)
            return Result<string>.Fail("Car already contains this value.", ErrorType.Conflict);

        var carExists = await carRepository.GetByIdAsync(command.CarId);
        if (carExists  == null)
            return Result<string>.Fail("Car not found", ErrorType.NotFound);

        var valueExists = await valueRepository.GetByIdAsync(command.ValueId);
        if (valueExists == null)
            return Result<string>.Fail("Value not found", ErrorType.NotFound);

        var carValue = command.ToEntity();

        await carValueRepository.CreateAsync(carValue);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "CarValue created successfully.");
    }
}