using RentalCar.Application.CarValues.Commands;
using RentalCar.Application.CarValues.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarValues.Handlers;

public class CarValueUpdateCommandHandler(
    ICarValueRepository carValueRepository,
    IUnitOfWork unitOfWork,
    IValidator<CarValueUpdateCommand> validator) 
    : ICommandHandler<CarValueUpdateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CarValueUpdateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result<string>.Fail(string.Join(";", validationResult.Errors.Select(s => s)), ErrorType.Validation);
        }
        
        var carValue = await carValueRepository.GetByIdAsync(command.CarId, command.ValueId);
        if (carValue == null)
        {
            return Result<string>.Fail("CarValue not found.", ErrorType.NotFound);
        }
        
        command.MapFrom(carValue);

        await carValueRepository.UpdateAsync(carValue);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "CarValue updated successfully.");
    }
}