using RentalCar.Application.CarAttributes.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarAttributes.Handlers;

public class CarAttributeDeleteCommandHandler(
    ICarAttributeRepository carAttributeRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CarAttributeDeleteCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CarAttributeDeleteCommand command)
    {
        var carAttribute = await carAttributeRepository.GetByIdAsync(command.Id);
        if (carAttribute == null)
        {
            return Result<string>.Fail("CarAttribute not found.", ErrorType.NotFound);
        }

        await carAttributeRepository.DeleteAsync(carAttribute);
        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "CarAttribute deleted successfully.");
    }
}