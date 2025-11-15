using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.CarAttributes.Mappers;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarAttributes.Handlers;

public class CarAttributeUpdateCommandHandler(ICarAttributeRepository carAttributeRepository,
    IUnitOfWork unitOfWork,
    IValidator<CarAttributeUpdateCommand> validator) : ICommandHandler<CarAttributeUpdateCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CarAttributeUpdateCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result<string>.Fail(string.Join(";", validationResult.Errors.Select(s => s)), ErrorType.Validation);
        }
        
        var student = await carAttributeRepository.GetByIdAsync(command.Id);
        if (student == null)
        {
            return Result<string>.Fail("CarAttribute not found.", ErrorType.NotFound);
        }
        
        command.MapFrom(student);

        await carAttributeRepository.UpdateAsync(student);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "CarAttribute updated successfully.");
    }
}