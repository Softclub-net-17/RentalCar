using RentalCar.Application.CarValues.Commands;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.CarValues.Handlers;

public class CarValueDeleteCommandHandler(
    ICarValueRepository carValueRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CarValueDeleteCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CarValueDeleteCommand command)
    {
        var carValue = await carValueRepository.GetByIdAsync(command.CarId, command.ValueId);
        if (carValue == null)
        {
            return Result<string>.Fail("CarValue not found.", ErrorType.NotFound);
        }

        await carValueRepository.DeleteAsync(carValue);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "CarValue deleted successfully.");
    }
}